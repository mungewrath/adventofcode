using Microsoft.Extensions.Logging;

public class Day_12 : IDay
{
    private ILogger<Day_12> _logger;
    public Day_12(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_12>();
    }

    // Intuition: Variation of island counting; we mark an island and fully explore it.
    // This can be done using BFS.
    // The perimeter can be calculated for any border with cells from a *different* island.
    // The area can be calculated as 1 per cell.
    // Time complexity O(W*H).
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        var map = ReadMap(lines);

        int height = lines.Length, width = lines[0].Length;
        int total = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                (int perim, int area) = StatsForAllotment(x, y, map);
                total += perim * area;
            }
        }

        return total;
    }

    private (int perimeter, int area) StatsForAllotment(int x, int y, PlantPatch[,] map)
    {
        if (map[x, y].Visited)
        {
            return (0, 0);
        }

        char patchType = map[x, y].Type;

        Queue<(int, int)> patchesToVisit = [];
        patchesToVisit.Enqueue((x, y));

        int perimeter = 0, area = 0;

        while (patchesToVisit.Count > 0)
        {
            (x, y) = patchesToVisit.Dequeue();

            if (x >= 0 && x < map.GetLength(1) &&
                y >= 0 && y < map.GetLength(0) &&
                map[x, y].Type == patchType)
            {
                if (map[x, y].Visited)
                {
                    continue;
                }
                // patchesToVisit.Enqueue((x, y2));
                map[x, y].Visited = true;
                area++;
                patchesToVisit.Enqueue((x, y - 1));
                patchesToVisit.Enqueue((x, y + 1));
                patchesToVisit.Enqueue((x - 1, y));
                patchesToVisit.Enqueue((x + 1, y));
            }
            else
            {
                // This square is not the same type,
                // or it's outside the map,
                // so it counts towards a border
                perimeter++;
            }
        }

        _logger.LogInformation("Finished surveying plot of {t}. Perimeter={p}, Area={a}", patchType, perimeter, area);

        return (perimeter, area);
    }

    private class PlantPatch
    {
        public bool Visited { get; set; } = false;
        public char Type { get; set; }
    }

    private PlantPatch[,] ReadMap(string[] lines)
    {
        var map = new PlantPatch[lines.Length, lines[0].Length];

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                map[y, x] = new PlantPatch
                {
                    Type = lines[y][x]
                };
            }
        }

        return map;
    }

    // Intuition: Same overall BFS approach as part one.
    // The way perimeter is calculated needs to be modified so it counts unique "sides"; we can express this as (x, y, direction).
    // E.g. (x=3,y=1,North) or (x=1, y=0,West)
    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        var map = ReadMap(lines);

        int height = lines.Length, width = lines[0].Length;
        int total = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                (int perim, int sides) = StatsForAllotmentWithSides(x, y, map);
                total += perim * sides;
            }
        }

        return total;
    }

    private enum Direction
    {
        North,
        South,
        East,
        West
    }

    private (int perimeter, int sides) StatsForAllotmentWithSides(int x, int y, PlantPatch[,] map)
    {
        if (map[x, y].Visited)
        {
            return (0, 0);
        }

        HashSet<(int x, int y, Direction dir)> sides = [];

        char patchType = map[x, y].Type;

        Queue<(int, int, Direction)> patchesToVisit = [];
        patchesToVisit.Enqueue((x, y, Direction.North));

        int area = 0;

        while (patchesToVisit.Count > 0)
        {
            (x, y, Direction dir) = patchesToVisit.Dequeue();

            if (x >= 0 && x < map.GetLength(1) &&
                y >= 0 && y < map.GetLength(0) &&
                map[x, y].Type == patchType)
            {
                if (map[x, y].Visited)
                {
                    continue;
                }
                // patchesToVisit.Enqueue((x, y2));
                map[x, y].Visited = true;
                area++;
                patchesToVisit.Enqueue((x, y - 1, Direction.North));
                patchesToVisit.Enqueue((x, y + 1, Direction.South));
                patchesToVisit.Enqueue((x - 1, y, Direction.West));
                patchesToVisit.Enqueue((x + 1, y, Direction.East));
            }
            else
            {
                // This square is not the same type,
                // or it's outside the map,
                // so it counts towards a border
                sides.Add((x, y, dir));
            }
        }

        _logger.LogInformation("Finished surveying plot of {t}. Sides={s}, Area={a}", patchType, sides.Count, area);

        // Consolidate distinct sides of the plot, with O(W*H) space
        int distinctSides = 0;
        while (sides.Count > 0)
        {
            var s = sides.Take(1).Single();
            sides.Remove(s);
            distinctSides++;

            // Remove all connected sides
            if (s.dir == Direction.North || s.dir == Direction.South)
            {
                int x_e = s.x + 1;
                int x_w = s.x - 1;
                while (sides.Contains((x_e, s.y, s.dir)))
                {
                    sides.Remove((x_e++, s.y, s.dir));
                }
                while (sides.Contains((x_w, s.y, s.dir)))
                {
                    sides.Remove((x_w--, s.y, s.dir));
                }
            }
            else
            {
                int y_n = s.y - 1;
                int y_s = s.y + 1;
                while (sides.Contains((s.x, y_n, s.dir)))
                {
                    sides.Remove((s.x, y_n--, s.dir));
                }
                while (sides.Contains((s.x, y_s, s.dir)))
                {
                    sides.Remove((s.x, y_s++, s.dir));
                }
            }
        }

        _logger.LogInformation("After consolidating sides, distinct sides={s}, Area={a}", distinctSides, area);

        return (distinctSides, area);
    }

}
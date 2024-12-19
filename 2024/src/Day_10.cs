using Microsoft.Extensions.Logging;

public class Day_10 : IDay
{
    private ILogger<Day_10> _logger;
    public Day_10(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_10>();
    }

    // Intuition: Simple hill climbing with DFS.
    // Theoretical complexity is high, but practically the search should end quickly for any given path.
    // O(W*H*4^10), for trying 4^10 possible combinations?
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        var map = ReadMap(lines);

        int trailheadScores = 0;

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 0)
                {
                    HashSet<(int, int)> trailheads = [];
                    int s = GetScore(x, y, map, trailheads, countDistinctPaths: false);
                    _logger.LogInformation("Trails starting at {x},{y} had a score of {s}", x, y, s);
                    trailheadScores += s;
                }
            }
        }


        return trailheadScores;
    }

    private int[,] ReadMap(string[] lines)
    {
        int[,] map = new int[lines.Length, lines[0].Length];

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                map[y, x] = lines[y][x] - '0';
            }
        }

        return map;
    }

    private int GetScore(int x, int y, int[,] map,
        HashSet<(int, int)> trailheads, bool countDistinctPaths, int height = 0)
    {
        if (height == 9 &&
        (countDistinctPaths || !trailheads.Contains((x, y))))
        {
            trailheads.Add((x, y));
            return 1;
        }

        int score = 0;

        for (int x2 = x - 1; x2 <= x + 1; x2++)
        {
            for (int y2 = y - 1; y2 <= y + 1; y2++)
            {
                if ((y2 < 0 || y2 >= map.GetLength(0) ||
                    x2 < 0 || x2 >= map.GetLength(1)) ||
                    !(x2 == x || y2 == y)) // Don't cross diagonals
                    continue;

                if (map[y2, x2] == height + 1)
                {
                    score += GetScore(x2, y2, map, trailheads,
                        countDistinctPaths, height + 1);
                }
            }
        }

        return score;
    }

    // Intuition: Actually the same as before, but all paths are being counted, not just the destinations
    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        var map = ReadMap(lines);

        int trailheadScores = 0;

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 0)
                {
                    HashSet<(int, int)> trailheads = [];
                    int s = GetScore(x, y, map, trailheads, countDistinctPaths: true);
                    _logger.LogInformation("Trails starting at {x},{y} had a score of {s}", x, y, s);
                    trailheadScores += s;
                }
            }
        }


        return trailheadScores;
    }
}
using Microsoft.Extensions.Logging;

public class Day_20 : IDay
{
    private ILogger<Day_20> _logger;
    private readonly int _cheatThreshold;

    public Day_20(ILoggerFactory factory, int cheatThreshold)
    {
        _logger = factory.CreateLogger<Day_20>();
        _cheatThreshold = cheatThreshold;
    }


    // Intuition: Since there is only a single track, we can traverse all of it and save the # of steps remaining until the finish
    // in O(W*H) time.
    // After that, we traverse a second time, simulating cheats from various points. This tells us how many steps we "skipped" with a cheat, and can tally them up.
    // Space complexity is O(W*H) as well.
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        var rt = new Racetrack(lines);

        long usefulCheatsFound = 0;

        foreach (KeyValuePair<(int, int), int> s in rt.StepsToFinish)
        {
            if (s.Value < _cheatThreshold)
            {
                // This won't save enough time, skip
                continue;
            }

            usefulCheatsFound += FindUsefulCheats(x: s.Key.Item1, y: s.Key.Item2, rt, maxDistance: 2);
        }

        return usefulCheatsFound;
    }

    private List<(int, int)> GenerateCoordinatesAtDistance(int x, int y, int distance)
    {
        List<(int, int)> coords = [];

        for (int dy = -distance; dy <= distance; dy++)
        {
            for (int dx = -distance; dx <= distance; dx++)
            {
                if (Math.Abs(dx) + Math.Abs(dy) == distance)
                {
                    coords.Add((x + dx, y + dy));
                }
            }
        }

        return coords;
    }

    private long FindUsefulCheats(int x, int y, Racetrack rt, int maxDistance)
    {
        long usefulCheatsFound = 0;

        List<(int, int)> coordsToTry = [];
        for (int i = 1; i <= maxDistance; i++)
        {
            coordsToTry.AddRange(GenerateCoordinatesAtDistance(x, y, i));
        }

        foreach ((int cx, int cy) in coordsToTry)
        {
            if (CheatSavesEnoughTime(x, y, rt, cx, cy))
            {
                usefulCheatsFound++;
            }
        }

        return usefulCheatsFound;
    }

    private bool CheatSavesEnoughTime(int x, int y, Racetrack rt, int cx, int cy)
    {
        return rt.StepsToFinish.ContainsKey((cx, cy)) &&
                rt.StepsToFinish[(x, y)] -
                rt.StepsToFinish[(cx, cy)] -
                Math.Abs(cx - x) -
                Math.Abs(cy - y)
                    >= _cheatThreshold;
    }

    public static readonly List<(int, int)> Directions =
    [
        (0, -1),
        (1, 0),
        (0, 1),
        (-1, 0)
    ];

    private class Racetrack
    {
        public readonly (int x, int y) Start;
        public readonly (int x, int y) End;

        public readonly char[,] Map;
        public readonly Dictionary<(int, int), int> StepsToFinish = [];

        public Racetrack(string[] lines)
        {
            int stepsLeft = 1;

            Map = new char[lines.Length, lines[0].Length];
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    Map[y, x] = lines[y][x];

                    if (lines[y][x] == '.')
                    {
                        stepsLeft++;
                    }
                    else if (lines[y][x] == 'S')
                    {
                        Start = (x, y);
                    }
                    else if (lines[y][x] == 'E')
                    {
                        End = (x, y);
                        // Simplifies special cases for routing
                        Map[y, x] = '.';
                    }
                }
            }

            (int xx, int yy) = Start;
            (int xp, int yp) = (0, 0);
            while (stepsLeft > 0)
            {
                StepsToFinish[(xx, yy)] = stepsLeft;

                bool foundNextStep = false;

                foreach ((int dx, int dy) in Directions)
                {
                    if (xx + dx < 0 || xx + dx > Map.GetLength(1) ||
                       yy + dy < 0 || yy + dy > Map.GetLength(0))
                    {
                        continue;
                    }

                    if (Map[yy + dy, xx + dx] == '.' && (xx + dx, yy + dy) != (xp, yp))
                    {
                        stepsLeft--;
                        xp = xx;
                        yp = yy;
                        xx = xx + dx;
                        yy = yy + dy;
                        foundNextStep = true;
                        break;
                    }
                }
                if (!foundNextStep)
                {
                    throw new Exception($"Could not find the next step with {stepsLeft} steps remaining. Last step was {xx},{yy}");
                }
            }
            StepsToFinish[(End.x, End.y)] = 0;
        }
    }

    // Intuition: Same overall approach as part one, but
    // it might be worth thinking of the time as O(W*H*L)
    // where L is the max length of a cheat.
    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        var rt = new Racetrack(lines);

        _logger.LogInformation("Identified {s} steps in the racetrack, including start/end", rt.StepsToFinish.Count);

        long usefulCheatsFound = 0;

        foreach (KeyValuePair<(int, int), int> s in rt.StepsToFinish)
        {
            // if (s.Value < _cheatThreshold)
            // {
            //     // This won't save enough time, skip
            //     continue;
            // }

            usefulCheatsFound += FindUsefulCheats(x: s.Key.Item1, y: s.Key.Item2, rt, maxDistance: 20);
        }

        return usefulCheatsFound;
    }
}
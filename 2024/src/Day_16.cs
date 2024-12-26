using Microsoft.Extensions.Logging;

public class Day_16 : IDay
{
    private ILogger<Day_16> _logger;
    private int endX;
    private int endY;
    private int startX;
    private int startY;
    private long minCost = long.MaxValue;

    public Day_16(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_16>();
    }

    // Intution: Use BFS to search the tree of choices iteratively. Need to use a heap/priority queue so routes with more hops (but fewer turns) are considered first.
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        var rt = ReadRacetrack(lines);

        return Bfs(startX, startY, endX, endY, Direction.East, rt);
    }

    private char[,] ReadRacetrack(string[] lines)
    {
        char[,] rt = new char[lines.Length, lines[0].Length];

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                rt[y, x] = lines[y][x];
                if (rt[y, x] == 'S')
                {
                    startX = x;
                    startY = y;
                }
                else if (rt[y, x] == 'E')
                {
                    endX = x;
                    endY = y;
                }
            }
        }

        return rt;
    }

    private enum Direction
    {
        North,
        East,
        South,
        West
    }

    private long Bfs(int startX, int startY, int endX, int endY, Direction startDir, char[,] rt)
    {
        PriorityQueue<(int x, int y, Direction d, long cost), long> q = new();
        long minCost = long.MaxValue;
        HashSet<(int, int, Direction)> visited = [];

        q.Enqueue((startX, startY, startDir, 0), 0);

        while (q.Count > 0)
        {
            (int x, int y, Direction d, long cost) = q.Dequeue();

            // _logger.LogInformation("x={x},y={y},v={visited}", x, y, visited.Count);

            if (cost > minCost || rt[y, x] == '#' || visited.Contains((x, y, d)))
            {
                continue;
            }

            if (x == endX && y == endY)
            {
                _logger.LogInformation("Found new route, with cost {cost}", cost);
                minCost = Math.Min(cost, minCost);
            }
            else
            {
                visited.Add((x, y, d));

                if (d == Direction.North)
                {
                    q.Enqueue((x, y - 1, d, cost + 1), cost + 1);
                }
                else
                {
                    q.Enqueue((x, y - 1, Direction.North, cost + 1001), cost + 1001);
                }
                if (d == Direction.East)
                {
                    q.Enqueue((x + 1, y, d, cost + 1), cost + 1);
                }
                else
                {
                    q.Enqueue((x + 1, y, Direction.East, cost + 1001), cost + 1001);
                }
                if (d == Direction.South)
                {
                    q.Enqueue((x, y + 1, d, cost + 1), cost + 1);
                }
                else
                {
                    q.Enqueue((x, y + 1, Direction.South, cost + 1001), cost + 1001);
                }
                if (d == Direction.West)
                {
                    q.Enqueue((x - 1, y, d, cost + 1), cost + 1);
                }
                else
                {
                    q.Enqueue((x - 1, y, Direction.West, cost + 1001), cost + 1001);
                }
            }
        }

        return minCost;
    }

    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }
}
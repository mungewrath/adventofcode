using System.Text;
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

        return Bfs(startX, startY, endX, endY, Direction.East, rt).minCost;
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

    private (long minCost, int numSeats) Bfs(int startX, int startY, int endX, int endY, Direction startDir, char[,] rt)
    {
        PriorityQueue<(int x, int y, Direction d, long cost, HashSet<(int, int)> path), long> q = new();
        long minCost = long.MaxValue;
        Dictionary<(int, int, Direction), long> visited = [];
        HashSet<(int, int)> seatsOnWinningPaths = [(startX, startY)];

        q.Enqueue((startX, startY, startDir, 0, []), 0);

        while (q.Count > 0)
        {
            (int x, int y, Direction d, long cost, HashSet<(int, int)> path) = q.Dequeue();

            // _logger.LogInformation("x={x},y={y},v={visited}", x, y, visited.Count);

            if (cost > minCost || rt[y, x] == '#' ||
                (visited.ContainsKey((x, y, d)) && visited[(x, y, d)] < cost))
            {
                continue;
            }

            if (x == endX && y == endY)
            {
                _logger.LogInformation("Found new route, with cost {cost}", cost);
                minCost = Math.Min(cost, minCost);
                foreach (var s in path)
                {
                    seatsOnWinningPaths.Add(s);
                }
            }
            else
            {
                visited[(x, y, d)] = cost;

                if (d == Direction.North)
                {
                    var p = new HashSet<(int, int)>(path)
                    {
                        (x, y - 1)
                    };
                    q.Enqueue((x, y - 1, d, cost + 1, p), cost + 1);
                }
                else
                {
                    var p = new HashSet<(int, int)>(path)
                    {
                        (x, y - 1)
                    };
                    q.Enqueue((x, y - 1, Direction.North, cost + 1001, p), cost + 1001);
                }
                if (d == Direction.East)
                {
                    var p = new HashSet<(int, int)>(path)
                    {
                        (x + 1, y)
                    };
                    q.Enqueue((x + 1, y, d, cost + 1, p), cost + 1);
                }
                else
                {
                    var p = new HashSet<(int, int)>(path)
                    {
                        (x +1 , y)
                    };
                    q.Enqueue((x + 1, y, Direction.East, cost + 1001, p), cost + 1001);
                }
                if (d == Direction.South)
                {
                    var p = new HashSet<(int, int)>(path)
                    {
                        (x, y + 1)
                    };
                    q.Enqueue((x, y + 1, d, cost + 1, p), cost + 1);
                }
                else
                {
                    var p = new HashSet<(int, int)>(path)
                    {
                        (x, y + 1)
                    };
                    q.Enqueue((x, y + 1, Direction.South, cost + 1001, p), cost + 1001);
                }
                if (d == Direction.West)
                {
                    var p = new HashSet<(int, int)>(path)
                    {
                        (x - 1, y)
                    };
                    q.Enqueue((x - 1, y, d, cost + 1, p), cost + 1);
                }
                else
                {
                    var p = new HashSet<(int, int)>(path)
                    {
                        (x - 1, y)
                    };
                    q.Enqueue((x - 1, y, Direction.West, cost + 1001, p), cost + 1001);
                }
            }
        }

        foreach ((int sx, int sy) in seatsOnWinningPaths)
        {
            rt[sy, sx] = 'O';
        }
        PrintRacetrack(rt);

        return (minCost, seatsOnWinningPaths.Count);
    }

    private void PrintRacetrack(char[,] rt)
    {
        StringBuilder sb = new();
        for (int y = 0; y < rt.GetLength(0); y++)
        {
            for (int x = 0; x < rt.GetLength(1); x++)
            {
                sb.Append(rt[y, x]);
            }
            sb.Append('\n');
        }
        _logger.LogInformation("{r}", sb.ToString());
    }

    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        var rt = ReadRacetrack(lines);

        return Bfs(startX, startY, endX, endY, Direction.East, rt).numSeats;
    }
}
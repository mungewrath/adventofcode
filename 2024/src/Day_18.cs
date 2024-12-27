using Microsoft.Extensions.Logging;

public class Day_18 : IDay
{
    private ILogger<Day_18> _logger;
    private char[,] _map;
    private int _simulateLength;
    private int _memorySize;

    public Day_18(ILoggerFactory factory, int memorySize, int simulateLength)
    {
        _logger = factory.CreateLogger<Day_18>();
        _map = new char[memorySize, memorySize];
        _memorySize = memorySize;
        _simulateLength = simulateLength;
    }

    // Intuition: Use BFS to find the shortest path to the exit after obstructions.
    // O(W*H) worst case complexity, from O(V*E), where V is the number of spaces and E is the number of connections.
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        for (int i = 0; i < _simulateLength; i++)
        {
            var split = lines[i].Split(",");
            int x = int.Parse(split[0]);
            int y = int.Parse(split[1]);
            _map[y, x] = '#';
        }

        return Bfs(0, 0, _memorySize - 1, _memorySize - 1, Direction.North, _map).minCost;
    }

    private enum Direction
    {
        North,
        East,
        South,
        West
    }

    private (long minCost, HashSet<(int, int)> path) Bfs(int startX, int startY, int endX, int endY, Direction startDir, char[,] rt)
    {
        PriorityQueue<(int x, int y, Direction d, long cost, HashSet<(int, int)> path), long> q = new();
        long minCost = long.MaxValue;
        Dictionary<(int, int, Direction), long> visited = [];
        HashSet<(int, int)> escapePath = [(startX, startY)];

        q.Enqueue((startX, startY, startDir, 0, []), 0);

        while (q.Count > 0)
        {
            (int x, int y, Direction d, long cost, HashSet<(int, int)> path) = q.Dequeue();

            // _logger.LogInformation("x={x},y={y},v={visited}", x, y, visited.Count);

            if (x < 0 || x >= rt.GetLength(1) ||
                y < 0 || y >= rt.GetLength(0) ||
                cost >= minCost || rt[y, x] == '#' ||
                visited.ContainsKey((x, y, d)))
            {
                continue;
            }

            if (x == endX && y == endY)
            {
                _logger.LogInformation("Found new route, with cost {cost}", cost);
                minCost = Math.Min(cost, minCost);
                escapePath = path;
            }
            else
            {
                visited[(x, y, d)] = cost;

                HashSet<(int, int)> p;

                p = [.. path, (x, y - 1)];
                q.Enqueue((x, y - 1, Direction.North, cost + 1, p), cost + 1);

                p = [.. path, (x + 1, y)];
                q.Enqueue((x + 1, y, Direction.East, cost + 1, p), cost + 1);

                p = [.. path, (x, y + 1)];
                q.Enqueue((x, y + 1, Direction.South, cost + 1, p), cost + 1);

                p = [.. path, (x - 1, y)];
                q.Enqueue((x - 1, y, Direction.West, cost + 1, p), cost + 1);
            }
        }

        return (minCost, escapePath);
    }

    // Intuition: After solving part 1, we need to do two more things:
    // Read more obstacles until one falls that blocks the current solution;
    // Run BFS again to see if any other escape is possible.
    // O(W*H*T), where T is the number of steps before becoming blocked.
    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        for (int i = 0; i < _simulateLength; i++)
        {
            var split = lines[i].Split(",");
            int x = int.Parse(split[0]);
            int y = int.Parse(split[1]);
            _map[y, x] = '#';
        }

        long minCost;
        HashSet<(int, int)> path;
        int t = _simulateLength - 1;
        int ox = 0, oy = 0;


        (minCost, path) = Bfs(0, 0, _memorySize - 1, _memorySize - 1, Direction.North, _map);

        while (minCost != long.MaxValue)
        {
            do
            {
                t++;
                var split = lines[t].Split(",");
                ox = int.Parse(split[0]);
                oy = int.Parse(split[1]);
                _map[oy, ox] = '#';
            } while (!path.Contains((ox, oy)));
            _logger.LogInformation("({x}, {y}) was in path", ox, oy);

            (minCost, path) = Bfs(0, 0, _memorySize - 1, _memorySize - 1, Direction.North, _map);
        }

        _logger.LogInformation("Obstacle blocking escape at t={t}: ({x}, {y})", t, ox, oy);

        return 0;
    }
}
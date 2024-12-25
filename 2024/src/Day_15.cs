using Microsoft.Extensions.Logging;

public class Day_15 : IDay
{
    private ILogger<Day_15> _logger;
    public Day_15(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_15>();
    }

    // Intuition: Basic state tracking on a map. O(M*W) where M is the number of moves, and W is the greater of width/height. This is because pushing the boxes requires up to W additional checks every move.
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        (var warehouse, var directions, int x, int y) = ParseWarehouse(lines);

        _logger.LogInformation("Initial state");
        PrintWarehouse(warehouse);

        foreach (Direction d in directions)
        {
            switch (d)
            {
                case Direction.North:
                    if (AttemptMove(x, y, 0, -1, warehouse))
                    {
                        y -= 1;
                    }
                    break;
                case Direction.East:
                    if (AttemptMove(x, y, 1, 0, warehouse))
                    {
                        x += 1;
                    }
                    break;
                case Direction.South:
                    if (AttemptMove(x, y, 0, 1, warehouse))
                    {
                        y += 1;
                    }
                    break;
                case Direction.West:
                    if (AttemptMove(x, y, -1, 0, warehouse))
                    {
                        x -= 1;
                    }
                    break;
            }

            // PrintWarehouse(warehouse);
        }

        return SumOfBoxCoords(warehouse);
    }

    private enum Direction
    {
        North,
        East,
        South,
        West
    }

    private enum Space
    {
        Empty = 0,
        Wall = 1,
        Box = 2,
        Robot = 3
    }

    private (List<List<Space>> warehouse, List<Direction> moves, int x, int y) ParseWarehouse(string[] lines)
    {
        int idx = 0;
        int x = 0, y = 0;

        List<List<Space>> warehouse = [];

        while (lines[idx] != string.Empty)
        {
            (var l, int? robotX) = ParseWarehouseLine(lines[idx]);

            if (robotX.HasValue)
            {
                x = robotX.Value;
                y = idx;
            }
            warehouse.Add(l);
            idx++;
        }

        idx++;

        return (warehouse, ParseDirections(string.Join("", lines[idx..])), x, y);
    }

    private (List<Space>, int? robotX) ParseWarehouseLine(string line)
    {
        int? robotX = null;

        var spaces = line.Select((c, idx) =>
        {
            switch (c)
            {
                case '#':
                    return Space.Wall;
                case 'O':
                    return Space.Box;
                case '@':
                    robotX = idx;
                    return Space.Robot;
                default:
                    return Space.Empty;
            }
        }).ToList();

        return (spaces, robotX);
    }

    private List<Direction> ParseDirections(string line)
    {
        return line.Select(c =>
        {
            return c switch
            {
                '<' => Direction.West,
                '>' => Direction.East,
                '^' => Direction.North,
                'v' => Direction.South,
                _ => throw new InvalidDataException(),
            };
        }).ToList();
    }

    private bool AttemptMove(int x, int y, int dx, int dy, List<List<Space>> warehouse)
    {
        if (x + dx < 0 || x + dx >= warehouse[1].Count ||
           y + dy < 0 || y + dy >= warehouse.Count)
        {
            return false;
        }

        if (warehouse[y + dy][x + dx] == Space.Wall)
        {
            return false;
        }
        if (CanPush(x + dx, y + dy, dx, dy, warehouse))
        {
            Push(x, y, dx, dy, warehouse);
            return true;
        }
        return false;
    }

    private bool CanPush(int x, int y, int dx, int dy, List<List<Space>> warehouse)
    {
        if (x + dx < 0 || x + dx >= warehouse[1].Count ||
           y + dy < 0 || y + dy >= warehouse.Count)
        {
            return false;
        }

        switch (warehouse[y][x])
        {
            case Space.Empty:
                return true;
            case Space.Box:
                return CanPush(x + dx, y + dy, dx, dy, warehouse);
            default:
                return false;
        }
    }

    private void Push(int x, int y, int dx, int dy, List<List<Space>> warehouse)
    {
        if (warehouse[y][x] == Space.Empty)
        {
            return;
        }
        Push(x + dx, y + dy, dx, dy, warehouse);
        warehouse[y + dy][x + dx] = warehouse[y][x];
        warehouse[y][x] = Space.Empty;
    }

    private void PrintWarehouse(List<List<Space>> warehouse)
    {
        _logger.LogInformation("\n{l}", string.Join("\n", warehouse.Select(l => new string(l.Select(Stringify).ToArray()))));
    }
    private static char Stringify(Space s)
    {
        return s switch
        {
            Space.Empty => '.',
            Space.Wall => '#',
            Space.Robot => 'R',
            Space.Box => 'O',
            _ => throw new InvalidOperationException(),
        };
    }

    private long SumOfBoxCoords(List<List<Space>> warehouse)
    {
        long total = 0;

        for (int y = 0; y < warehouse.Count; y++)
        {
            for (int x = 0; x < warehouse[y].Count; x++)
            {
                if (warehouse[y][x] == Space.Box)
                {
                    total += y * 100 + x;
                }
            }
        }

        return total;
    }

    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }
}
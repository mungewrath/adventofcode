using Microsoft.Extensions.Logging;

public class Day_6 : IDay
{
    private ILogger<Day_6> _logger;
    public Day_6(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_6>();
    }

    // Intuition: Straightforward play-through-the-steps puzzle;
    // not sure about upper bound complexity on this one because you can re-cross the same spot many times. But unless there's a cycle, each run across the map would get you at least one more spot, so perhaps ≈O(W*(H^2)) or O((W^2)*H)?
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        bool[,] visited = new bool[lines.Length, lines[0].Length];
        // Assume the guard is always starting going up
        int dirX = 0, dirY = -1;
        (int x, int y) = GetStartingCoordinates(lines);

        while (true)
        {
            visited[y, x] = true;

            if (y + dirY < 0 || y + dirY >= lines.Length ||
               x + dirX < 0 || x + dirX >= lines[y + dirY].Length)
            {
                break;
            }

            if (lines[y + dirY][x + dirX] == '#')
            {
                (dirX, dirY) = RotateClockwise(dirX, dirY);
            }
            else
            {
                _logger.LogInformation("Moving from {x}, {y} to {x2}, {y2}", x, y, dirX, dirY);
                x += dirX;
                y += dirY;
            }
        }

        return CountSquaresVisited(visited);
    }

    private (int, int) GetStartingCoordinates(string[] lines)
    {
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (lines[y][x] == '^')
                {
                    return (x, y);
                }
            }
        }

        throw new InvalidOperationException("Guard did not appear anywhere in the maze!");
    }

    private (int, int) RotateClockwise(int dirX, int dirY)
    {
        if (dirY == 1)
        {
            return (-1, 0);
        }
        else if (dirY == -1)
        {
            return (1, 0);
        }
        else if (dirX == 1)
        {
            return (0, 1);
        }
        else
        {
            return (0, -1);
        }
    }

    private int CountSquaresVisited(bool[,] visited)
    {
        int total = 0;
        for (int y = 0; y < visited.GetLength(0); y++)
        {
            for (int x = 0; x < visited.GetLength(1); x++)
            {
                total += visited[y, x] ? 1 : 0;
            }
        }

        return total;
    }

    // Intuition: Brute force iterate through the space, trying an obstacle at every position.
    // Originally tried to do this more intelligently but there were too many edge cases.
    // The runtime is pretty bad though.
    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        HashSet<(int, int)> obstacles = [];

        char[,] map = new char[lines.Length, lines[0].Length];
        for (int j = 0; j < lines.Length; j++)
        {
            for (int i = 0; i < lines[j].Length; i++)
            {
                map[j, i] = lines[j][i];
            }
        }

        // Assume the guard is always starting going up
        int dirX = 0, dirY = -1;
        (int x, int y) = GetStartingCoordinates(lines);

        for (int j = 0; j < lines.Length; j++)
        {
            for (int i = 0; i < lines[j].Length; i++)
            {
                if (map[j, i] == '#')
                    continue;

                map[j, i] = '#';
                if (!obstacles.Contains((x + dirX, y + dirY)) && HasCycle(x, y, dirX, dirY, map))
                {
                    _logger.LogInformation("Added obstacle at {x}, {y}", i, j);
                    obstacles.Add((i, j));

                }
                map[j, i] = '.';
            }
        }

        return obstacles.Count;
    }

    private bool HasCycle(int x, int y,
        int dirX, int dirY, char[,] map)
    {
        HashSet<(int, int, int, int)> positionsAndDirs = [];

        while (true)
        {
            if (positionsAndDirs.Contains((x, y, dirX, dirY)))
            {
                return true;
            }

            positionsAndDirs.Add((x, y, dirX, dirY));

            if (y + dirY < 0 || y + dirY >= map.GetLength(0) ||
               x + dirX < 0 || x + dirX >= map.GetLength(1))
            {
                break;
            }

            if (map[y + dirY, x + dirX] == '#')
            {
                (dirX, dirY) = RotateClockwise(dirX, dirY);
                // _logger.LogInformation("Rotating. New direction {x2}, {y2}", dirX, dirY);
            }
            else
            {
                // _logger.LogInformation("Moving from {x}, {y} to {x2}, {y2}", x, y, x + dirX, y + dirY);
                x += dirX;
                y += dirY;
            }
        }

        return false;
    }
}
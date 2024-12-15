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
    public int Solve(string inputPath)
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

    public int SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }
}
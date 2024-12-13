
using Microsoft.Extensions.Logging;

public class Day_4 : IDay
{
    private ILogger<Day_4> _logger;
    public Day_4(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_4>();
    }

    // Intuition: Iterating through the matrix of characters, you can try all 8 directions for an XMAS spelling. This is O(N) since XMAS is a constant.
    public int Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        int total = 0;

        for (int y = 0; y < lines.Count(); y++)
        {
            string row = lines[y];
            for (int x = 0; x < row.Length; x++)
            {
                if (row[x] == 'X')
                {
                    total += FindXmas(0, -1, lines, x, y) // Up
                        + FindXmas(1, -1, lines, x, y) // Up Right
                        + FindXmas(1, 0, lines, x, y) // Right
                        + FindXmas(1, 1, lines, x, y) // Down Right
                        + FindXmas(0, 1, lines, x, y) // Down
                        + FindXmas(-1, 1, lines, x, y) // Down Left
                        + FindXmas(-1, 0, lines, x, y) // Left
                        + FindXmas(-1, -1, lines, x, y); // Up Left
                }
            }
        }

        return total;
    }

    private int FindXmas(int xDir, int yDir, string[] lines, int x, int y)
    {
        if (0 <= y + (3 * yDir) && y + (3 * yDir) < lines.Count())
        {
            if (0 <= x + (3 * xDir) && x + (3 * xDir) < lines[y].Length)
            {
                if (
                    lines[y + yDir][x + xDir] == 'M' &&
                    lines[y + yDir * 2][x + xDir * 2] == 'A' &&
                    lines[y + yDir * 3][x + xDir * 3] == 'S'
                )
                {
                    return 1;
                }
            }
        }

        return 0;
    }

    // Intuition: Similar to part one. Iterate through the input but now we are looking for the 'A' which is the center.
    public int SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        int total = 0;

        for (int y = 0; y < lines.Count(); y++)
        {
            string row = lines[y];
            for (int x = 0; x < row.Length; x++)
            {
                if (row[x] == 'A')
                {
                    total += FindMasX(lines, x, y);
                }
            }
        }

        return total;
    }

    private int FindMasX(string[] lines, int x, int y)
    {
        if (0 <= y - 1 && y + 1 < lines.Count())
        {
            if (0 <= x - 1 && x + 1 < lines[y].Length)
            {
                if (
                    ((lines[y - 1][x - 1] == 'M' && lines[y + 1][x + 1] == 'S') ||
                    (lines[y - 1][x - 1] == 'S' && lines[y + 1][x + 1] == 'M'))
                    &&
                    ((lines[y + 1][x - 1] == 'M' && lines[y - 1][x + 1] == 'S') ||
                    (lines[y + 1][x - 1] == 'S' && lines[y - 1][x + 1] == 'M'))
                )
                {
                    return 1;
                }
            }
        }

        return 0;
    }
}
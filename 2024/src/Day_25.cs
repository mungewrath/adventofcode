using Microsoft.Extensions.Logging;

public class Day_25 : IDay
{
    private ILogger<Day_25> _logger;
    public Day_25(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_25>();
    }

    // Intuition: We can't do exact hash search because keys can still fit if the lock has wiggle room.
    // Start with a brute force approach that tries all combinations in O(L*K) time.
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        (List<int[]> locks, List<int[]> keys) = ParseSchematics(lines);

        long validPairs = 0;

        // Check each lock against each key
        foreach (var lockHeights in locks)
        {
            foreach (var keyHeights in keys)
            {
                if (IsValidPair(lockHeights, keyHeights))
                {
                    validPairs++;
                }
            }
        }

        return validPairs;
    }

    private static (List<int[]> locks, List<int[]> keys) ParseSchematics(string[] schematics)
    {
        List<int[]> locks = [];
        List<int[]> keys = [];
        int width = schematics[0].Length;
        int height = 7;

        for (int i = 0; i < schematics.Length; i += height + 1)
        {
            int[] columnHeights = new int[width];
            bool isLock = schematics[i][0] == '#';

            for (int col = 0; col < width; col++)
            {
                if (isLock)
                {
                    // Count downward '#' from the top
                    columnHeights[col] = CountHashes(schematics, i, height, col);
                }
                else
                {
                    // Count upward '#' from the bottom
                    columnHeights[col] = CountHashes(schematics, i, height, col, reverse: true);
                }
            }
            if (isLock)
            {
                locks.Add(columnHeights);
            }
            else
            {
                keys.Add(columnHeights);
            }
        }

        return (locks, keys);
    }

    private static int CountHashes(string[] schematics, int start, int height, int col, bool reverse = false)
    {
        int count = 0;
        if (reverse)
        {
            for (int row = start + height - 1; row >= start; row--)
            {
                if (schematics[row][col] == '#')
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
        }
        else
        {
            for (int row = start; row < start + height; row++)
            {
                if (schematics[row][col] == '#')
                {
                    count++;
                }
                else
                {
                    break;
                }
            }
        }
        return count;
    }

    private static bool IsValidPair(int[] lockHeights, int[] keyHeights)
    {
        for (int i = 0; i < lockHeights.Length; i++)
        {
            // Maximum of 5 overlap, with a lock always having an extra top row and key having extra bottom row
            if (lockHeights[i] + keyHeights[i] > 7)
            {
                return false;
            }
        }
        return true;
    }

    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }
}
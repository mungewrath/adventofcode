using Microsoft.Extensions.Logging;

public class Day_22 : IDay
{
    private ILogger<Day_22> _logger;
    private readonly int _iterations;

    public Day_22(ILoggerFactory factory, int iterations)
    {
        _logger = factory.CreateLogger<Day_22>();
        _iterations = iterations;
    }

    // Intuition: Use bitwise transforms to do the calculations quickly, which is O(N)
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        List<long> startingSecrets = lines.Select(long.Parse).ToList();

        return startingSecrets.Sum(ss => Transform(ss, _iterations));
    }

    private static long Transform(long ss, int iterations)
    {
        for (int i = 0; i < iterations; i++)
        {
            ss = ((ss << 6) ^ ss) % (1 << 24);
            ss = ((ss / (1 << 5)) ^ ss) % (1 << 24);
            ss = ((ss << 11) ^ ss) % (1 << 24);
        }

        return ss;
    }

    // Intuition: As we traverse the list of secrets and generate new ones,
    // we can also keep track of the sequence of 4 past numbers, mod 10, for extra space complexity (19^4 integers). Keeping track of the max we can quickly return it in O(N) time.
    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        List<long> startingSecrets = lines.Select(long.Parse).ToList();

        long[,,,] salesForSeq = new long[19, 19, 19, 19];
        long maxSale = 0;

        foreach (long ss in startingSecrets)
        {
            maxSale = Math.Max(maxSale, SellToMonkey(ss, salesForSeq, maxSale));
        }

        return maxSale;
    }

    private long SellToMonkey(long ss, long[,,,] salesForSeq, long maxSale)
    {
        LinkedList<int> seq = [];

        long curr = ss;

        HashSet<(int, int, int, int)> saleMomentsUsed = [];

        for (int i = 0; i < _iterations; i++)
        {
            long prev = curr;
            curr = Transform(curr, 1);
            seq.AddLast((int)(curr % 10) - (int)(prev % 10));
            if (seq.Count > 4)
            {
                seq.RemoveFirst();
            }
            if (seq.Count == 4)
            {
                var l = seq.Select(n => n).ToList();

                var key = (l[0] + 9, l[1] + 9, l[2] + 9, l[3] + 9);

                // We already sold to this monkey using this seq
                if (saleMomentsUsed.Contains(key))
                {
                    continue;
                }

                saleMomentsUsed.Add(key);

                salesForSeq[l[0] + 9, l[1] + 9, l[2] + 9, l[3] + 9] += (int)(curr % 10);
                maxSale = Math.Max(maxSale, salesForSeq[l[0] + 9, l[1] + 9, l[2] + 9, l[3] + 9]);
            }
        }

        return maxSale;
    }
}
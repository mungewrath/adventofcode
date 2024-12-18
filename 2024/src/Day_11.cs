using Microsoft.Extensions.Logging;

public class Day_11 : IDay
{
    private ILogger<Day_11> _logger;
    public Day_11(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_11>();
    }

    // Intuition: since the stones are splitting in-place, we want a linked list.
    // For every blink, this is O(N), but N grows each time. So pessimistically... ≈O(N*2^B)?
    public long Solve(string inputPath)
    {
        LinkedList<long> stones = new(File.ReadAllLines(inputPath).Single().Split(" ").Select(long.Parse));

        for (int i = 0; i < 25; i++)
        {
            BlinkStones(stones);
        }

        return stones.Count;
    }

    private static void BlinkStones(LinkedList<long> stones)
    {
        LinkedListNode<long>? curr = stones.First;

        while (curr != null)
        {
            if (curr.Value == 0)
            {
                curr.Value = 1;
            }
            else if (curr.Value.ToString().Length % 2 == 0)
            {
                string parent = curr.Value.ToString();
                long firstHalf = long.Parse(parent[..(parent.Length / 2)]);
                long secondHalf = long.Parse(parent[(parent.Length / 2)..]);

                curr.Value = firstHalf;
                stones.AddAfter(curr, secondHalf);
                // Skip the second-half stone since it was just created
                curr = curr.Next;
            }
            else
            {
                curr.Value *= 2024;
            }
            curr = curr.Next;
        }
    }

    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }
}
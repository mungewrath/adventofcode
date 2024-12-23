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

    // Got stuck on this one. After looking up other answers, counting the total stones by number was most intuitive to me.
    // Their positions don't matter since each multiplies in isolation, only the total counts matter.
    // The number of distinct values is also limited due to how they divide (don't ask me to prove this)
    public long SolvePartTwo(string inputPath)
    {
        List<long> stones = new(File.ReadAllLines(inputPath).Single().Split(" ").Select(long.Parse));
        Dictionary<long, long> countsByNum = stones.ToDictionary(s => s, s => (long)1);

        for (int t = 0; t < 75; t++)
        {
            Dictionary<long, long> nextCount = [];

            foreach (KeyValuePair<long, long> kv in countsByNum)
            {
                if (kv.Key == 0)
                {
                    if (!nextCount.ContainsKey(1))
                    {
                        nextCount[1] = 0;
                    }
                    nextCount[1] += kv.Value;
                }
                else if (kv.Key.ToString().Length % 2 == 0)
                {
                    string parent = kv.Key.ToString();
                    long firstHalf = long.Parse(parent[..(parent.Length / 2)]);
                    long secondHalf = long.Parse(parent[(parent.Length / 2)..]);

                    if (!nextCount.ContainsKey(firstHalf))
                    {
                        nextCount[firstHalf] = 0;
                    }
                    nextCount[firstHalf] += kv.Value;

                    if (!nextCount.ContainsKey(secondHalf))
                    {
                        nextCount[secondHalf] = 0;
                    }
                    nextCount[secondHalf] += kv.Value;
                }
                else
                {
                    if (!nextCount.ContainsKey(kv.Key * 2024))
                    {
                        nextCount[kv.Key * 2024] = 0;
                    }
                    nextCount[kv.Key * 2024] += kv.Value;
                }
            }

            _logger.LogInformation("At t={t}, had {n} stones", t, nextCount.Values.Sum());

            countsByNum = nextCount;
        }

        return countsByNum.Values.Sum();
    }
}
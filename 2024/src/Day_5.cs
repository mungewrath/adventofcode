using Microsoft.Extensions.Logging;

public class Day_5 : IDay
{
    private ILogger<Day_5> _logger;
    public Day_5(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_5>();
    }

    // Intuition: the core problem is tracking that for X|Y, X came _before_ Y.
    // As we traverse, we can keep a list of all prior numbers, so it is easy to tell if rules were satisfied.
    // The other part is managing the rules in play. They are triggered by Y, so index them based on Y.
    // All of this is O(N*R) using hash maps, where R is the size of the ruleset.
    public int Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        int sumOfMiddlePages = 0;

        (Dictionary<int, List<int>> rules, List<List<int>> pageNumberSets) = ReadRevisions(lines);

        _logger.LogInformation("rules={Rules}", rules);
        _logger.LogInformation("pageNumberSets={pageNumberSets}", pageNumberSets);

        foreach (List<int> pageNums in pageNumberSets)
        {
            if (CorrectlyOrdered(pageNums, rules))
            {
                sumOfMiddlePages += pageNums[pageNums.Count / 2];
            }
        }

        return sumOfMiddlePages;
    }

    private (Dictionary<int, List<int>>, List<List<int>>) ReadRevisions(string[] lines)
    {
        int i = 0;
        Dictionary<int, List<int>> rules = [];
        List<List<int>> pageNumberSets = [];

        while (lines[i] != "")
        {
            string[] split = lines[i].Split("|");
            int key = int.Parse(split[1]);
            int prior = int.Parse(split[0]);

            if (!rules.ContainsKey(key))
            {
                rules[key] = [];
            }
            rules[key].Add(prior);

            i++;
        }

        // Skip the empty middle line
        i++;

        while (i < lines.Length)
        {
            pageNumberSets.Add(lines[i].Split(",").Select(n => int.Parse(n)).ToList());
            i++;
        }

        return (rules, pageNumberSets);
    }

    private bool CorrectlyOrdered(List<int> pageNums, Dictionary<int, List<int>> rules)
    {
        HashSet<int> previousNums = [];
        HashSet<int> allNums = pageNums.ToHashSet();

        foreach (int n in pageNums)
        {
            if (rules.ContainsKey(n))
            {
                foreach (int mustBePrior in rules[n])
                {
                    if (!previousNums.Contains(mustBePrior) && allNums.Contains(mustBePrior))
                    {
                        return false;
                    }
                }
            }

            previousNums.Add(n);
        }

        return true;
    }

    public int SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }
}
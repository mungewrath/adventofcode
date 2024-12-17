using Microsoft.Extensions.Logging;

public class Day_5 : IDay
{
    private ILogger<Day_5> _logger;
    private ILoggerFactory _loggerFactory;
    public Day_5(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_5>();
        _loggerFactory = factory;
    }

    // Intuition: the core problem is tracking that for X|Y, X came _before_ Y.
    // As we traverse, we can keep a list of all prior numbers, so it is easy to tell if rules were satisfied.
    // The other part is managing the rules in play. They are triggered by Y, so index them based on Y.
    // All of this is O(N*R) using hash maps, where R is the size of the ruleset.
    public long Solve(string inputPath)
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

    // Intuition: Suspected this at first, and after researching it is a directed acyclic graph (DAG). This can be solved using depth first search. O(N+R) according to wikipedia.
    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        int total = 0;


        (Dictionary<int, List<int>> rules, List<List<int>> pageNumberSets) = ReadRevisions(lines);

        _logger.LogInformation("rules={Rules}", rules);
        _logger.LogInformation("pageNumberSets={pageNumberSets}", pageNumberSets);

        foreach (List<int> pages in pageNumberSets)
        {
            _logger.LogInformation("Solving for the set of numbers: {nums}", string.Join(",", pages));

            if (CorrectlyOrdered(pages, rules))
            {
                _logger.LogInformation("The set is already sorted, skipping");
                continue;
            }

            var dag = new DirectedAcyclicGraph(dependencies: rules, pages, _loggerFactory);

            var sorted = dag.TopographicalSort();
            _logger.LogInformation("Received topographically sorted pages as {pages}", string.Join(",", sorted));

            total += sorted[sorted.Count / 2];
        }

        return total;
    }
}
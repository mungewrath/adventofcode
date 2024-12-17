using Microsoft.Extensions.Logging;

public class Day_7 : IDay
{
    private ILogger<Day_7> _logger;
    public Day_7(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_7>();
    }

    // Intuition: Brute force the combinations, DFS.
    // O(2^N), where N is the number of terms in the equation.
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        var equations = ReadEquations(lines);
        long satisfiableSum = 0;

        foreach (Equation e in equations)
        {
            if (Satisfiable(e))
            {
                _logger.LogInformation("Equation was satisfiable. Adding total {total}", e.Total);
                satisfiableSum += e.Total;
            }
        }

        return satisfiableSum;
    }

    private List<Equation> ReadEquations(string[] lines)
    {
        List<Equation> equations = [];

        foreach (string l in lines)
        {
            var split = l.Split(":");
            equations.Add(new()
            {
                Total = long.Parse(split[0]),
                Terms = split[1].Trim().Split(' ').Select(long.Parse).ToList()
            });
        }

        return equations;
    }

    private bool Satisfiable(Equation e, int idx = 1, long total = 0, bool useConcat = false)
    {
        if (total > e.Total)
        {
            return false;
        }
        if (idx == e.Terms.Count)
        {
            return total == e.Total;
        }

        if (total == 0)
        {
            total = e.Terms[0];
        }

        bool result =
            Satisfiable(e, idx + 1, total + e.Terms[idx], useConcat) || Satisfiable(e, idx + 1, total * e.Terms[idx], useConcat);

        if (useConcat)
        {
            result |= Satisfiable(e, idx + 1, long.Parse(total.ToString() + e.Terms[idx].ToString()), useConcat);
        }

        return result;
    }

    private class Equation
    {
        public long Total;
        public List<long> Terms;
    }

    // Intuition: Same approach, just with a third branch.
    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        var equations = ReadEquations(lines);
        long satisfiableSum = 0;

        foreach (Equation e in equations)
        {
            if (Satisfiable(e, useConcat: true))
            {
                _logger.LogInformation("Equation was satisfiable. Adding total {total}", e.Total);
                satisfiableSum += e.Total;
            }
        }

        return satisfiableSum;
    }
}
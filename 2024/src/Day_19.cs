using System.Data.SqlTypes;
using System.Text;
using Microsoft.Extensions.Logging;

public class Day_19 : IDay
{
    private ILogger<Day_19> _logger;
    private Dictionary<string, bool> _memoized;

    public Day_19(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_19>();
        _memoized = [];
    }


    // Intuition: Feels like a dynamic programming case, so solve using recursion (DFS?).
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        (List<string> towels, List<string> designsNeeded) = ReadRequest(lines);

        return designsNeeded.Select(d => IsPossible(d, towels, new StringBuilder())).Count(p => p == true);
    }

    private (List<string> towels, List<string> designsNeeded) ReadRequest(string[] lines)
    {
        var towels = lines[0].Split(", ").OrderByDescending(s => s.Length).ToList();

        var designsNeeded = lines[2..].ToList();

        return (towels, designsNeeded);
    }

    private bool IsPossible(string design, List<string> towels, StringBuilder sb)
    {
        if (_memoized.ContainsKey(design[sb.Length..]))
        {
            return _memoized[design[sb.Length..]];
        }

        if (sb.Length == design.Length)
        {
            _memoized.Add(design, true);
            return true;
        }

        foreach (string t in towels)
        {
            if (sb.Length + t.Length > design.Length)
            {
                continue;
            }

            bool matches = true;
            for (int i = 0; i < t.Length; i++)
            {
                if (design[sb.Length + i] != t[i])
                {
                    matches = false;
                    break;
                }
            }

            if (matches)
            {
                sb.Append(t);
                if (IsPossible(design, towels, sb))
                {
                    return true;
                }
                else
                {
                    sb.Remove(sb.Length - t.Length, t.Length);
                }
            }
        }

        _memoized[design[sb.Length..]] = false;
        return false;
    }

    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }
}
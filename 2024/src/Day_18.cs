using Microsoft.Extensions.Logging;

public class Day_18 : IDay
{
    private ILogger<Day_18> _logger;
    public Day_18(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_18>();
    }

    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }

    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }
}
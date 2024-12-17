using Microsoft.Extensions.Logging;

public class Day_12 : IDay
{
    private ILogger<Day_12> _logger;
    public Day_12(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_12>();
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
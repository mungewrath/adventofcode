using Microsoft.Extensions.Logging;

public class Day_25 : IDay
{
    private ILogger<Day_25> _logger;
    public Day_25(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_25>();
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
using Microsoft.Extensions.Logging;

public class Day_15 : IDay
{
    private ILogger<Day_15> _logger;
    public Day_15(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_15>();
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
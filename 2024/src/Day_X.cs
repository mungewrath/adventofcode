using Microsoft.Extensions.Logging;

public class Day_X : IDay
{
    private ILogger<Day_X> _logger;
    public Day_X(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_X>();
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
using Microsoft.Extensions.Logging;

public class Day_13 : IDay
{
    private ILogger<Day_13> _logger;
    public Day_13(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_13>();
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
using Microsoft.Extensions.Logging;

public class Day_16 : IDay
{
    private ILogger<Day_16> _logger;
    public Day_16(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_16>();
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
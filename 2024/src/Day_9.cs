using Microsoft.Extensions.Logging;

public class Day_9 : IDay
{
    private ILogger<Day_9> _logger;
    public Day_9(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_9>();
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
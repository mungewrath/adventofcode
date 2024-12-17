using Microsoft.Extensions.Logging;

public class Day_14 : IDay
{
    private ILogger<Day_14> _logger;
    public Day_14(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_14>();
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
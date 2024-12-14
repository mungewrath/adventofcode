using Microsoft.Extensions.Logging;

public class Day_10 : IDay
{
    private ILogger<Day_10> _logger;
    public Day_10(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_10>();
    }

    public int Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }

    public int SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }
}
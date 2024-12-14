using Microsoft.Extensions.Logging;

public class Day_17 : IDay
{
    private ILogger<Day_17> _logger;
    public Day_17(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_17>();
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
using Microsoft.Extensions.Logging;

public class Day_20 : IDay
{
    private ILogger<Day_20> _logger;
    public Day_20(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_20>();
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
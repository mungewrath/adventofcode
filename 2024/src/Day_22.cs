using Microsoft.Extensions.Logging;

public class Day_22 : IDay
{
    private ILogger<Day_22> _logger;
    public Day_22(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_22>();
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
using Microsoft.Extensions.Logging;

public class Day_23 : IDay
{
    private ILogger<Day_23> _logger;
    public Day_23(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_23>();
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
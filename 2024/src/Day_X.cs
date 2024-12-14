using Microsoft.Extensions.Logging;

public class Day_X : IDay
{
    private ILogger<Day_X> _logger;
    public Day_X(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_X>();
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
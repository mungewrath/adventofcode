using Microsoft.Extensions.Logging;

public class Day_11 : IDay
{
    private ILogger<Day_11> _logger;
    public Day_11(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_11>();
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
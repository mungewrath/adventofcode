using Microsoft.Extensions.Logging;

public class Day_21 : IDay
{
    private ILogger<Day_21> _logger;
    public Day_21(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_21>();
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
using Microsoft.Extensions.Logging;

public class Day_19 : IDay
{
    private ILogger<Day_19> _logger;
    public Day_19(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_19>();
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
using Microsoft.Extensions.Logging;

public class Day_6 : IDay
{
    private ILogger<Day_6> _logger;
    public Day_6(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_6>();
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
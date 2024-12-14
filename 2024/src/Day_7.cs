using Microsoft.Extensions.Logging;

public class Day_7 : IDay
{
    private ILogger<Day_7> _logger;
    public Day_7(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_7>();
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
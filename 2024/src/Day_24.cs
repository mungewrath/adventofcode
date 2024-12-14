using Microsoft.Extensions.Logging;

public class Day_24 : IDay
{
    private ILogger<Day_24> _logger;
    public Day_24(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_24>();
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
using Microsoft.Extensions.Logging;

public class Day_8 : IDay
{
    private ILogger<Day_8> _logger;
    public Day_8(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_8>();
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
using Microsoft.Extensions.Logging;

public class Day_22 : IDay
{
    private ILogger<Day_22> _logger;
    private readonly int _iterations;

    public Day_22(ILoggerFactory factory, int iterations)
    {
        _logger = factory.CreateLogger<Day_22>();
        _iterations = iterations;
    }

    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        List<long> startingSecrets = lines.Select(long.Parse).ToList();

        return startingSecrets.Sum(ss => Transform(ss, _iterations));
    }

    private static long Transform(long ss, int iterations)
    {
        for (int i = 0; i < iterations; i++)
        {
            ss = ((ss << 6) ^ ss) % (1 << 24);
            ss = ((ss / (1 << 5)) ^ ss) % (1 << 24);
            ss = ((ss << 11) ^ ss) % (1 << 24);
        }

        return ss;
    }

    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }
}
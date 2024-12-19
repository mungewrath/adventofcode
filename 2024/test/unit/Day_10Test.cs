namespace unit;
using Microsoft.Extensions.Logging;

public class Day_10Test
{
    [Fact]
    public void Solve_CountsOnlyPeaksFromTrailheads()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_10 solver = new(factory);

        long result = solver.Solve("day_10_sample.txt");

        Assert.Equal(36, result);
    }

    [Fact]
    public void SolvePartTwo_CountsAllDistinctPaths()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_10 solver = new(factory);

        long result = solver.SolvePartTwo("day_10_sample.txt");

        Assert.Equal(81, result);
    }
}
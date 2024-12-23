namespace unit;
using Microsoft.Extensions.Logging;

public class Day_14Test
{
    [Fact]
    public void Solve_BucketsRobotsIntoCorrectQuadrants()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_14 solver = new(factory);

        long result = solver.Solve("day_14_sample.txt");

        Assert.Equal(12, result);
    }

    [Fact]
    public void SolvePartTwo_()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_14 solver = new(factory);

        long result = solver.SolvePartTwo("day_14_sample.txt");

        Assert.Equal(31, result);
    }
}
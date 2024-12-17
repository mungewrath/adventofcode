namespace unit;
using Microsoft.Extensions.Logging;

public class Day_15Test
{
    [Fact]
    public void Solve_()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_15 solver = new(factory);

        long result = solver.Solve("day_15_sample.txt");

        Assert.Equal(11, result);
    }

    [Fact]
    public void SolvePartTwo_()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_15 solver = new(factory);

        long result = solver.SolvePartTwo("day_15_sample.txt");

        Assert.Equal(31, result);
    }
}
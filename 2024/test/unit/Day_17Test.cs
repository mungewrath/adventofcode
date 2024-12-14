namespace unit;
using Microsoft.Extensions.Logging;

public class Day_17Test
{
    [Fact]
    public void Solve_()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_17 solver = new(factory);

        int result = solver.Solve("day_17_sample.txt");

        Assert.Equal(11, result);
    }

    [Fact]
    public void SolvePartTwo_()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_17 solver = new(factory);

        int result = solver.SolvePartTwo("day_17_sample.txt");

        Assert.Equal(31, result);
    }
}
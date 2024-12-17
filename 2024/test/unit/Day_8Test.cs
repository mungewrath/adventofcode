namespace unit;
using Microsoft.Extensions.Logging;

public class Day_8Test
{
    [Fact]
    public void Solve_()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_8 solver = new(factory);

        long result = solver.Solve("day_8_sample.txt");

        Assert.Equal(14, result);
    }

    [Fact]
    public void SolvePartTwo_()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_8 solver = new(factory);

        long result = solver.SolvePartTwo("day_8_sample.txt");

        Assert.Equal(34, result);
    }
}
namespace unit;
using Microsoft.Extensions.Logging;

public class Day_21Test
{
    [Fact]
    public void Solve_()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_21 solver = new(factory);

        long result = solver.Solve("day_21_sample.txt");

        Assert.Equal(11, result);
    }

    [Fact]
    public void SolvePartTwo_()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_21 solver = new(factory);

        long result = solver.SolvePartTwo("day_21_sample.txt");

        Assert.Equal(31, result);
    }
}
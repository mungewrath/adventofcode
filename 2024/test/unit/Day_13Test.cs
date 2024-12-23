namespace unit;
using Microsoft.Extensions.Logging;

public class Day_13Test
{
    [Fact]
    public void Solve_CalculatesWinningComboCosts()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_13 solver = new(factory);

        long result = solver.Solve("day_13_sample.txt");

        Assert.Equal(480, result);
    }

    [Fact]
    public void SolvePartTwo_()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_13 solver = new(factory);

        long result = solver.SolvePartTwo("day_13_sample.txt");

        Assert.Equal(875318608908, result);
    }
}
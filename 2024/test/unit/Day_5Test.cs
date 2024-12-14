namespace unit;
using Microsoft.Extensions.Logging;

public class Day_5Test
{
    [Fact]
    public void Solve_IdentifiesCorrectSets()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_5 solver = new(factory);

        int result = solver.Solve("day_5_sample.txt");

        Assert.Equal(143, result);
    }

    [Fact]
    public void SolvePartTwo_()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_5 solver = new(factory);

        int result = solver.SolvePartTwo("day_5_sample.txt");

        Assert.Equal(31, result);
    }
}
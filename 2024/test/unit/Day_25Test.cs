namespace unit;
using Microsoft.Extensions.Logging;

public class Day_25Test
{
    [Fact]
    public void Solve_IdentifiesAllValidCombinations()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_25 solver = new(factory);

        long result = solver.Solve("day_25_sample.txt");

        Assert.Equal(3, result);
    }

    [Fact]
    public void SolvePartTwo_()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_25 solver = new(factory);

        long result = solver.SolvePartTwo("day_25_sample.txt");

        Assert.Equal(31, result);
    }
}
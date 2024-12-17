namespace unit;
using Microsoft.Extensions.Logging;

public class Day_7Test
{
    [Fact]
    public void Solve_SumsSatisfiableEquationTotals()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_7 solver = new(factory);

        long result = solver.Solve("day_7_sample.txt");

        Assert.Equal(3749, result);
    }

    [Fact]
    public void SolvePartTwo_SumsSatisfiableEquationTotalsWithConcat()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_7 solver = new(factory);

        long result = solver.SolvePartTwo("day_7_sample.txt");

        Assert.Equal(11387, result);
    }
}
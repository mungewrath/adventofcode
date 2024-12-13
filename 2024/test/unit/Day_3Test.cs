namespace unit;
using Microsoft.Extensions.Logging;

public class Day_3Test
{
    [Fact]
    public void Solve_IdentifiesMulInstructions()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_3 solver = new(factory);

        int result = solver.Solve("day_3_sample.txt");

        Assert.Equal(161, result);
    }

    [Fact]
    public void SolvePartTwo_HandlesDoAndDont()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_3 solver = new(factory);

        int result = solver.SolvePartTwo("day_3p2_sample.txt");

        Assert.Equal(48, result);
    }
}
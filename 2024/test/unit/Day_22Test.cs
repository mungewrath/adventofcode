namespace unit;
using Microsoft.Extensions.Logging;

public class Day_22Test
{
    [Fact]
    public void Solve_GeneratesCorrectPseudorandoms()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_22 solver = new(factory, 2000);

        long result = solver.Solve("day_22_sample.txt");

        Assert.Equal(37327623, result);
    }

    [Fact]
    public void SolvePartTwo_FindsMaxSaleForSingleMonkey()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_22 solver = new(factory, 10);

        long result = solver.SolvePartTwo("day_22_sample2.txt");

        Assert.Equal(6, result);
    }

    [Fact]
    public void SolvePartTwo_FindsMaxSaleForManyMonkeys()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_22 solver = new(factory, 2000);

        long result = solver.SolvePartTwo("day_22_sample3.txt");

        Assert.Equal(23, result);
    }
}
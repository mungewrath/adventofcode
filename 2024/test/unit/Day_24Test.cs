namespace unit;
using Microsoft.Extensions.Logging;

public class Day_24Test
{
    [Fact]
    public void Solve_SimulatesCircuitCorrectly()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_24 solver = new(factory);

        long result = solver.Solve("day_24_sample.txt");

        Assert.Equal(4, result);
    }

    [Fact]
    public void Solve_SimulatesLargerCircuitCorrectly()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_24 solver = new(factory);

        long result = solver.Solve("day_24_sample2.txt");

        Assert.Equal(2024, result);
    }

    [Fact]
    public void SolvePartTwo_()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_24 solver = new(factory);

        long result = solver.SolvePartTwo("day_24_sample.txt");

        Assert.Equal(31, result);
    }
}
namespace unit;
using Microsoft.Extensions.Logging;

public class Day_20Test
{
    [Fact]
    public void Solve_CountsAllDistinctCheatsAboveThreshold()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_20 solver = new(factory, 10);

        long result = solver.Solve("day_20_sample.txt");

        Assert.Equal(10, result);
    }

    [Fact]
    public void Solve_CountsAllDistinctCheatsAboveHighThreshold()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_20 solver = new(factory, 20);

        long result = solver.Solve("day_20_sample.txt");

        Assert.Equal(5, result);
    }

    // [Fact]
    // public void SolvePartTwo_()
    // {
    //     using ILoggerFactory factory = LoggerFactory.Create(builder => { });
    //     Day_20 solver = new(factory);

    //     long result = solver.SolvePartTwo("day_20_sample.txt");

    //     Assert.Equal(31, result);
    // }
}
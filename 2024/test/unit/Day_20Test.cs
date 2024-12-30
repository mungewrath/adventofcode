namespace unit;
using Microsoft.Extensions.Logging;

public class Day_20Test
{
    [Fact]
    public void Solve_CountsAllDistinctCheatsAboveThreshold()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_20 solver = new(factory, cheatThreshold: 10);

        long result = solver.Solve("day_20_sample.txt");

        Assert.Equal(10, result);
    }

    [Fact]
    public void Solve_CountsAllDistinctCheatsAboveHighThreshold()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_20 solver = new(factory, cheatThreshold: 20);

        long result = solver.Solve("day_20_sample.txt");

        Assert.Equal(5, result);
    }

    [Fact]
    public void SolvePartTwo_CountsAllDistinctCheatsWithLongerCheatLength()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_20 solver = new(factory, cheatThreshold: 60);

        long result = solver.SolvePartTwo("day_20_sample.txt");

        Assert.Equal(129, result);
    }

    [Fact]
    public void SolvePartTwo_CountsAllDistinctCheatsWithLongerCheatLengthAndHighThreshold()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_20 solver = new(factory, cheatThreshold: 72);

        long result = solver.SolvePartTwo("day_20_sample.txt");

        Assert.Equal(29, result);
    }
}
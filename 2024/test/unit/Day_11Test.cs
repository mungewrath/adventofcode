namespace unit;
using Microsoft.Extensions.Logging;

public class Day_11Test
{
    [Fact]
    public void Solve_SimulatesStonesCorrectly()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_11 solver = new(factory);

        long result = solver.Solve("day_11_sample.txt");

        Assert.Equal(55312, result);
    }

    [Fact]
    public void SolvePartTwo_TracksCountsCorrectly()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_11 solver = new(factory);

        long result = solver.SolvePartTwo("day_11_sample.txt");

        Assert.Equal(65601038650482, result);
    }
}
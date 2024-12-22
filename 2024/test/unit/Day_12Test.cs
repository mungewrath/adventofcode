namespace unit;
using Microsoft.Extensions.Logging;

public class Day_12Test
{
    [Fact]
    public void Solve_CountsPerimeterAndArea()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_12 solver = new(factory);

        long result = solver.Solve("day_12_sample.txt");

        Assert.Equal(1930, result);
    }

    [Fact]
    public void SolvePartTwo_CountsSidesAndArea()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_12 solver = new(factory);

        long result = solver.SolvePartTwo("day_12_sample.txt");

        Assert.Equal(1206, result);
    }
}
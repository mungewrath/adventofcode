namespace unit;
using Microsoft.Extensions.Logging;

public class Day_4Test
{
    [Fact]
    public void Solve_FindsXmas()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_4 solver = new(factory);

        long result = solver.Solve("day_4_sample.txt");

        Assert.Equal(18, result);
    }

    [Fact]
    public void SolvePartTwo_FindsMasX()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_4 solver = new(factory);

        long result = solver.SolvePartTwo("day_4_sample.txt");

        Assert.Equal(9, result);
    }
}
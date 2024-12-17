namespace unit;
using Microsoft.Extensions.Logging;

public class Day_2Test
{
    [Fact]
    public void Solve_IdentifiesCorrectNumberOfSafeReports()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_2 solver = new(factory);

        long result = solver.Solve("day_2_sample.txt");

        Assert.Equal(2, result);
    }

    [Fact]
    public void SolvePartTwo_ReturnsTotalNumberOfIntersections()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_2 solver = new(factory);

        long result = solver.SolvePartTwo("day_2_sample.txt");

        Assert.Equal(4, result);
    }
}
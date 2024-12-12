namespace unit;
using Microsoft.Extensions.Logging;

public class Day_1Test
{
    [Fact]
    public void Solve_ReturnsSortedDifferences()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_1 solver = new(factory);

        int result = solver.Solve("sample.txt");

        Assert.Equal(11, result);
    }

    [Fact]
    public void SolvePartTwo_ReturnsTotalNumberOfIntersections()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_1 solver = new(factory);

        int result = solver.SolvePartTwo("sample.txt");

        Assert.Equal(31, result);
    }
}
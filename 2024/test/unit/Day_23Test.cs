namespace unit;
using Microsoft.Extensions.Logging;

public class Day_23Test
{
    [Fact]
    public void Solve_FindsMatchingSetsWithTNAme()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_23 solver = new(factory);

        long result = solver.Solve("day_23_sample.txt");

        Assert.Equal(7, result);
    }
}
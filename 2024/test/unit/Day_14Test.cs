namespace unit;
using Microsoft.Extensions.Logging;

public class Day_14Test
{
    [Fact]
    public void Solve_BucketsRobotsIntoCorrectQuadrants()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_14 solver = new(factory, width: 11, height: 7);

        long result = solver.Solve("day_14_sample.txt");

        Assert.Equal(12, result);
    }
}
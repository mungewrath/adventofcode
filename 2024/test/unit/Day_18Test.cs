namespace unit;
using Microsoft.Extensions.Logging;

public class Day_18Test
{
    [Fact]
    public void Solve_CountsStepsInShortestPath()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_18 solver = new(factory, 7, 12);

        long result = solver.Solve("day_18_sample.txt");

        Assert.Equal(22, result);
    }

    // [Fact]
    // public void SolvePartTwo_()
    // {
    //     using ILoggerFactory factory = LoggerFactory.Create(builder => { });
    //     Day_18 solver = new(factory);

    //     long result = solver.SolvePartTwo("day_18_sample.txt");

    //     Assert.Equal(31, result);
    // }
}
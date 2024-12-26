namespace unit;
using Microsoft.Extensions.Logging;

public class Day_16Test
{
    [Fact]
    public void Solve_FindsOptimalPath()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_16 solver = new(factory);

        long result = solver.Solve("day_16_sample.txt");

        Assert.Equal(7036, result);
    }

    [Fact]
    public void Solve_FindsOptimalPath2()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_16 solver = new(factory);

        long result = solver.Solve("day_16_sample2.txt");

        Assert.Equal(11048, result);
    }

    [Fact]
    public void SolvePartTwo_FindsAllSeatsOnEveryBestPath()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddConsole()
                .AddFile("logs/app-{Date}.log");
        });
        Day_16 solver = new(factory);

        long result = solver.SolvePartTwo("day_16_sample.txt");

        Assert.Equal(45, result);
    }
}
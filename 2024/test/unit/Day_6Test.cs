namespace unit;
using Microsoft.Extensions.Logging;

public class Day_6Test
{
    [Fact]
    public void Solve_TracesMazeCorrectly()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_6 solver = new(factory);

        int result = solver.Solve("day_6_sample.txt");

        Assert.Equal(41, result);
    }

    [Fact]
    public void SolvePartTwo_()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddConsole()
                .AddFile("logs/app-{Date}.log");
        });
        Day_6 solver = new(factory);

        int result = solver.SolvePartTwo("day_6_sample.txt");

        Assert.Equal(6, result);
    }
}
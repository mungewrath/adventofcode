namespace unit;
using Microsoft.Extensions.Logging;

public class Day_15Test
{
    [Fact]
    public void Solve_CorrectlyMovesBoxes()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddConsole()
                .AddFile("logs/app-{Date}.log");
        });
        Day_15 solver = new(factory);

        long result = solver.Solve("day_15_sample.txt");

        Assert.Equal(10092, result);
    }

    [Fact]
    public void Solve_Small_CorrectlyMovesBoxes()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddConsole()
                .AddFile("logs/app-{Date}.log");
        });
        Day_15 solver = new(factory);

        long result = solver.Solve("day_15_sample_small.txt");

        Assert.Equal(2028, result);
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
        Day_15 solver = new(factory);

        long result = solver.SolvePartTwo("day_15_sample.txt");

        Assert.Equal(31, result);
    }
}
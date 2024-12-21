namespace unit;
using Microsoft.Extensions.Logging;

public class Day_9Test
{
    [Fact]
    public void Solve_MovesBitsToFreeSpace()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_9 solver = new(factory);

        long result = solver.Solve("day_9_sample.txt");

        Assert.Equal(1928, result);
    }

    [Fact]
    public void SolvePartTwo_MovesFilesToFreeSpace()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddConsole()
                .AddFile("logs/app-{Date}.log");
        });
        Day_9 solver = new(factory);

        long result = solver.SolvePartTwo("day_9_sample.txt");

        Assert.Equal(2858, result);
    }
}
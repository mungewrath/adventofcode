namespace unit;
using Microsoft.Extensions.Logging;

public class Day_17Test
{
    [Fact]
    public void Solve_InterpretsProgramCorrectly()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddConsole()
                .AddFile("logs/app-{Date}.log");
        });
        Day_17 solver = new(factory);

        long result = solver.Solve("day_17_sample.txt");

        Assert.Equal(4635635210, result);
    }

    [Fact]
    public void SolvePartTwo_FindsSelfReplicatingAValue()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_17 solver = new(factory);

        long result = solver.SolvePartTwo("day_17_sample2.txt");

        Assert.Equal(117440, result);
    }
}
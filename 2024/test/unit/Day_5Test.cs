namespace unit;
using Microsoft.Extensions.Logging;

public class Day_5Test
{
    [Fact]
    public void Solve_IdentifiesCorrectSets()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_5 solver = new(factory);

        long result = solver.Solve("day_5_sample.txt");

        Assert.Equal(143, result);
    }

    [Fact]
    public void SolvePartTwo_SortsOutOfOrderPageSets()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddConsole()
                .AddFile("logs/app-{Date}.log");
        });
        Day_5 solver = new(factory);

        long result = solver.SolvePartTwo("day_5_sample.txt");

        Assert.Equal(123, result);
    }
}
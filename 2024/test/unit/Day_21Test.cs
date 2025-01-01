namespace unit;
using Microsoft.Extensions.Logging;

public class Day_21Test
{
    [Fact]
    public void Solve_TranslatesToHumanPressesOptimally()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder => { });
        Day_21 solver = new(factory, 2);

        long result = solver.Solve("day_21_sample.txt");

        Assert.Equal(126384, result);
    }

    [Fact]
    public void SolvePartTwo_TranslatesToHumanPressesOptimally()
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddConsole()
                .AddFile("logs/app-{Date}.log");
        });
        Day_21 solver = new(factory, 2);

        long result = solver.SolvePartTwo("day_21_sample.txt");

        Assert.Equal(126384, result);
    }
}
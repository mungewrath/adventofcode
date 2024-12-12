using Microsoft.Extensions.Logging;

public class AdventOfCode
{
    public static void Main(string[] args)
    {
        using ILoggerFactory factory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddConsole()
                .AddFile("logs/app-{Date}.log");

        });

        ILogger logger = factory.CreateLogger<AdventOfCode>();
        logger.LogInformation("Hello World! Logging is {Description}.", "fun");

        var solver = new Day_1(factory);
        solver.Solve(args[1]);
        int partTwoAnswer = solver.SolvePartTwo(args[1]);
    }
}



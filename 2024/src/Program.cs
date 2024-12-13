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

        IDay solver = CreateSolver(int.Parse(args[0]), factory);

        int answer = solver.Solve(args[1]);
        logger.LogInformation("Received answer for part one: {answer}", answer);
        int partTwoAnswer = solver.SolvePartTwo(args[1]);
        logger.LogInformation("Received answer for part two: {partTwoAnswer}", partTwoAnswer);
    }

    private static IDay CreateSolver(int day, ILoggerFactory loggerFactory)
    {
        switch (day)
        {
            case 1:
                return new Day_1(loggerFactory);
            case 2:
                return new Day_2(loggerFactory);
            case 3:
                return new Day_3(loggerFactory);
            default:
                throw new NotImplementedException("Invalid day specified");
        }
    }
}



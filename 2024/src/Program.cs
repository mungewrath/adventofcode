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

        long answer = solver.Solve(args[1]);
        logger.LogInformation("Received answer for part one: {answer}", answer);

        IDay solver2 = CreateSolver(int.Parse(args[0]), factory);
        long partTwoAnswer = solver2.SolvePartTwo(args[1]);
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
            case 4:
                return new Day_4(loggerFactory);
            case 5:
                return new Day_5(loggerFactory);
            case 6:
                return new Day_6(loggerFactory);
            case 7:
                return new Day_7(loggerFactory);
            case 8:
                return new Day_8(loggerFactory);
            case 9:
                return new Day_9(loggerFactory);
            case 10:
                return new Day_10(loggerFactory);
            case 11:
                return new Day_11(loggerFactory);
            case 12:
                return new Day_12(loggerFactory);
            case 13:
                return new Day_13(loggerFactory);
            case 14:
                return new Day_14(loggerFactory, width: 101, height: 103);
            case 15:
                return new Day_15(loggerFactory);
            case 16:
                return new Day_16(loggerFactory);
            case 17:
                return new Day_17(loggerFactory);
            case 18:
                return new Day_18(loggerFactory, memorySize: 71, simulateLength: 1024);
            case 19:
                return new Day_19(loggerFactory);
            case 20:
                return new Day_20(loggerFactory);
            case 21:
                return new Day_21(loggerFactory);
            case 22:
                return new Day_22(loggerFactory);
            case 23:
                return new Day_23(loggerFactory);
            case 24:
                return new Day_24(loggerFactory);
            case 25:
                return new Day_25(loggerFactory);
            default:
                throw new NotImplementedException("Invalid day specified");
        }
    }
}



using Microsoft.Extensions.Logging;
using Serilog;

public class Day_14 : IDay
{
    private ILogger<Day_14> _logger;
    private readonly int _width;
    private readonly int _height;
    public Day_14(ILoggerFactory factory, int width, int height)
    {
        _logger = factory.CreateLogger<Day_14>();
        _width = width;
        _height = height;
    }

    // Intuition: Since the robot wraps around, it's the same as mod.
    // This can be calculated in O(N) time for N robots.
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        var robots = ReadRobots(lines);

        int[] quadrantTotals = new int[4];

        return CalculateRisk(100, robots);
    }

    private List<Robot> ReadRobots(string[] lines)
    {
        List<Robot> robots = [];
        //p=0,4 v=3,-3
        foreach (string line in lines)
        {
            var split = line.Split(" ");
            string[] position = split[0].Split("=")[1].Split(",");
            string[] velocity = split[1].Split("=")[1].Split(",");
            robots.Add(new()
            {
                X = int.Parse(position[0]),
                Y = int.Parse(position[1]),
                Dx = int.Parse(velocity[0]),
                Dy = int.Parse(velocity[1])
            });
        }

        return robots;
    }

    public enum Quadrant
    {
        None = -1,
        UpperLeft = 0,
        UpperRight = 1,
        LowerLeft = 2,
        LowerRight = 3
    };

    private class Robot
    {
        public int X;
        public int Y;
        public int Dx;
        public int Dy;


        public Quadrant GetQuadrant(int seconds, int width, int height)
        {
            (int fx, int fy) = FinalPosition(seconds, width, height);
            if (fx == width / 2 || fy == height / 2)
            {
                return Quadrant.None;
            }
            else if (fx <= width / 2 && fy <= height / 2)
            {
                return Quadrant.UpperLeft;
            }
            else if (fx > width / 2 && fy <= height / 2)
            {
                return Quadrant.UpperRight;
            }
            else if (fx <= width / 2 && fy > height / 2)
            {
                return Quadrant.LowerLeft;
            }
            else
            {
                return Quadrant.LowerRight;
            }
        }

        private (int x, int y) FinalPosition(int seconds, int width, int height)
        {
            (int fx, int fy) = (
                (X + Dx * seconds) % width,
                (Y + Dy * seconds) % height
            );

            if (fx < 0)
            {
                fx += width;
            }
            if (fy < 0)
            {
                fy += height;
            }

            return (fx, fy);
        }
    }

    // Ok, I had to look this one up. Possibly a coincidence that 
    // the minimum risk coincides with the easter egg formation.
    // O(N*T), where T is the number of times being sampled.
    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        var robots = ReadRobots(lines);

        long minRisk = long.MaxValue;
        long minRiskSeconds = 0;

        for (int t = 0; t < 10_000; t++)
        {
            long risk = CalculateRisk(seconds: t, robots);
            if (risk < minRisk)
            {
                minRiskSeconds = t;
                minRisk = risk;
            }
        }

        return minRiskSeconds;
    }

    private long CalculateRisk(int seconds, List<Robot> robots)
    {
        int[] quadrantTotals = new int[4];

        foreach (Robot r in robots)
        {
            Quadrant q = r.GetQuadrant(seconds: seconds, _width, _height);
            // _logger.LogInformation("Placed robot in quadrant {q}", q.ToString());
            if (q == Quadrant.None)
            {
                continue;
            }

            quadrantTotals[(int)q] += 1;
        }

        return quadrantTotals.Aggregate(1, (acc, x) => acc * x);
    }
}
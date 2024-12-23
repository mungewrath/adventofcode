using Microsoft.Extensions.Logging;

public class Day_14 : IDay
{
    private ILogger<Day_14> _logger;
    public Day_14(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_14>();
    }

    // Intuition: Since the robot wraps around, it's the same as mod.
    // This can be calculated in O(N) time for N robots.
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        var robots = ReadRobots(lines);

        int[] quadrantTotals = new int[4];

        foreach (Robot r in robots)
        {
            Quadrant q = r.GetQuadrant(seconds: 100);
            _logger.LogInformation("Placed robot in quadrant {q}", q.ToString());
            if (q == Quadrant.None)
            {
                continue;
            }

            quadrantTotals[(int)q] += 1;
        }

        return quadrantTotals.Aggregate(1, (acc, x) => acc * x);
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

        private const int _width = 101;
        private const int _height = 103;

        public Quadrant GetQuadrant(int seconds)
        {
            (int fx, int fy) = FinalPosition(seconds);
            if (fx == _width / 2 || fy == _height / 2)
            {
                return Quadrant.None;
            }
            else if (fx <= _width / 2 && fy <= _height / 2)
            {
                return Quadrant.UpperLeft;
            }
            else if (fx > _width / 2 && fy <= _height / 2)
            {
                return Quadrant.UpperRight;
            }
            else if (fx <= _width / 2 && fy > _height / 2)
            {
                return Quadrant.LowerLeft;
            }
            else
            {
                return Quadrant.LowerRight;
            }
        }

        private (int x, int y) FinalPosition(int seconds)
        {
            (int fx, int fy) = (
                (X + Dx * seconds) % _width,
                (Y + Dy * seconds) % _height
            );

            if (fx < 0)
            {
                fx += _width;
            }
            if (fy < 0)
            {
                fy += _height;
            }

            return (fx, fy);
        }
    }

    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }
}
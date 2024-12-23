using Microsoft.Extensions.Logging;

public class Day_13 : IDay
{
    private ILogger<Day_13> _logger;
    public Day_13(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_13>();
    }

    // Intuition: two equations, two unknowns means this has a closed-form solution.
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        var machines = ParseFile(lines);

        long totalTokensSpent = 0;

        foreach (ClawMachine m in machines)
        {
            if (CalculateWinningCombo(m, max: 100, out long ap, out long bp))
            {
                if (m.A1 * ap + m.B1 * bp != m.X ||
                   m.A2 * ap + m.B2 * bp != m.Y)
                {
                    _logger.LogError("Wrong solution identified for {ap},{bp} => {x} {y}. Disaster", ap, bp, m.X, m.Y);
                }

                _logger.LogInformation("Won a prize pressing Ax{a} and Bx{b}", ap, bp);
                totalTokensSpent += ap * 3 + bp;
            }
        }

        return totalTokensSpent;
    }

    private class ClawMachine
    {
        public long A1;
        public long B1;
        public long X;
        public long A2;
        public long B2;
        public long Y;

        public long GetVariableSide(long b)
        {
            return A2 * (X - B1 * b) + B2 * b * A1;
        }
    }

    private List<ClawMachine> ParseFile(string[] lines, long correction = 0)
    {
        List<ClawMachine> machines = [];

        // Incrementing by 4 each time since there is an empty line in between machines
        for (int i = 0; i < lines.Length; i += 4)
        {
            ClawMachine m = new();

            string[] coeffsA = lines[i].Split(":")[1].Split(",");
            m.A1 = long.Parse(coeffsA[0].Split("+")[1]);
            m.A2 = long.Parse(coeffsA[1].Split("+")[1]);
            string[] coeffsB = lines[i + 1].Split(":")[1].Split(",");
            m.B1 = long.Parse(coeffsB[0].Split("+")[1]);
            m.B2 = long.Parse(coeffsB[1].Split("+")[1]);
            string[] prizeCoords = lines[i + 2].Split(":")[1].Split(",");
            m.X = long.Parse(prizeCoords[0].Split("=")[1]) + correction;
            m.Y = long.Parse(prizeCoords[1].Split("=")[1]) + correction;

            machines.Add(m);
        }

        return machines;
    }

    // Pull the coefficients into an equation that you can plug in different numbers of b for.
    // Example:
    // 94a + 22b = 8400
    // ^a1   ^b1   ^X
    // 34a + 67b = 5400
    // ^a2   ^b2   ^Y
    //
    // 94a = 8400 - 22b
    // a = (8400 - 22b)/94
    //
    // 34*(8400 - 22b)/94 + 67b = 5400
    // 34*(8400 - 22b) + 67b*94 = 5400*94
    // ^a2  ^x    ^b1    ^b2 ^a1   ^y  ^a1
    //
    // We know the bounds are 0 <= b <= 100, so it can be solved with binary search in O(log N) time.
    private bool CalculateWinningCombo(ClawMachine m, long max, out long aPresses, out long bPresses)
    {
        bool found = false;
        aPresses = bPresses = 0;
        bool negativeTrend = m.GetVariableSide(1) > m.GetVariableSide(2);

        long min = 0;

        while (min <= max)
        {
            long i = min + (max - min) / 2;

            long varSide = m.GetVariableSide(i);
            long numSide = m.Y * m.A1;

            if (varSide == numSide)
            {
                bPresses = i;
                found = true;
                break;
            }
            else if (varSide > numSide == !negativeTrend)
            {
                // Too high
                max = i - 1;
            }
            else
            {
                // Too low
                min = i + 1;
            }
        }

        if (found && (m.X - m.B1 * bPresses) % m.A1 == 0)
        {
            aPresses = (m.X - m.B1 * bPresses) / m.A1;
            return true;
        }

        return false;
    }

    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        var machines = ParseFile(lines, correction: 10_000_000_000_000);

        long totalTokensSpent = 0;

        foreach (ClawMachine m in machines)
        {
            if (CalculateWinningCombo(m, max: 10_000_000_000_000, out long ap, out long bp))
            {
                if (m.A1 * ap + m.B1 * bp != m.X ||
                   m.A2 * ap + m.B2 * bp != m.Y)
                {
                    _logger.LogError("Wrong solution identified for {ap},{bp} => {x} {y}. Disaster", ap, bp, m.X, m.Y);
                }

                _logger.LogInformation("Won a prize pressing Ax{a} and Bx{b}", ap, bp);
                totalTokensSpent += ap * 3 + bp;
            }
        }

        return totalTokensSpent;
    }
}
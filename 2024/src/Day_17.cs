using Microsoft.Extensions.Logging;

public class Day_17 : IDay
{
    private ILogger<Day_17> _logger;
    private long a;
    private long b;
    private long c;

    public Day_17(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_17>();
    }

    // This isn't so much an algorithm as an interpreter implementation. So, no time complexity per se.
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        a = long.Parse(lines[0].Split(": ")[1]);
        b = long.Parse(lines[1].Split(": ")[1]);
        c = long.Parse(lines[2].Split(": ")[1]);

        List<int> program = lines[4].Split(": ")[1].Split(",").Select(int.Parse).ToList();
        List<int> outputs = Simulate(program);

        _logger.LogInformation("Final output: {o}", string.Join(",", outputs));

        return long.Parse(string.Join("", outputs));
    }

    private List<int> Simulate(List<int> program, bool shouldReplicate = false)
    {
        List<int> outputs = [];

        int pos = 0;

        while (pos < program.Count)
        {
            int instruction = program[pos++];
            int operand = program[pos++];

            _logger.LogInformation("ins={instruction};op={operand};pos={pos}", instruction, operand, pos);

            switch (instruction)
            {
                case 0: // adv
                    a = a / ((int)Math.Pow(2, GetOperandValue(operand)));
                    break;
                case 1: // bxl
                    b = b ^ operand;
                    break;
                case 2: // bst
                    b = GetOperandValue(operand) % 8;
                    break;
                case 3: // jnz
                    if (a != 0)
                    {
                        pos = operand;
                    }
                    break;
                case 4: // bxc
                    b = b ^ c;
                    break;
                case 5: // out
                    outputs.Add((int)(GetOperandValue(operand) % 8));
                    if (shouldReplicate && outputs[outputs.Count - 1] != program[outputs.Count - 1])
                    {
                        // End early because we are diverging
                        return outputs;
                    }
                    break;
                case 6: // bdv
                    b = a / ((int)Math.Pow(2, GetOperandValue(operand)));
                    break;
                case 7: // cdv
                    c = a / ((int)Math.Pow(2, GetOperandValue(operand)));
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        return outputs;
    }

    private long GetOperandValue(int operand)
    {
        if (operand < 4)
        {
            return operand;
        }
        else if (operand == 4)
        {
            return a;
        }
        else if (operand == 5)
        {
            return b;
        }
        else if (operand == 6)
        {
            return c;
        }
        else
        {
            throw new InvalidOperationException();
        }

    }

    // Got stuck on this one. After looking up hints, break it down by working backwards:
    // Each number output uses only 3 bits at a time for the input program we're trying to reproduce.
    // So after finding the starting value of a needed to produce each output, starting at the end, shift left 3 bits.
    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        a = long.Parse(lines[0].Split(": ")[1]);
        b = long.Parse(lines[1].Split(": ")[1]);
        c = long.Parse(lines[2].Split(": ")[1]);

        List<int> program = lines[4].Split(": ")[1].Split(",").Select(int.Parse).ToList();

        List<int> outputs = [];
        long aStart = -1;

        for (int idx = program.Count - 1; idx >= 0; idx--)
        {
            while (!string.Join(",", outputs).Equals(string.Join(",", program[idx..])))
            {
                aStart++;
                a = aStart;
                b = c = 0;
                outputs = Simulate(program);
            }

            aStart <<= 3;
            aStart -= 1;
        }

        aStart += 1;
        aStart >>= 3;

        _logger.LogInformation("Final output: {o}", string.Join(",", outputs));

        return aStart;
    }
}
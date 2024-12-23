using Microsoft.Extensions.Logging;

public class Day_17 : IDay
{
    private ILogger<Day_17> _logger;
    private int a;
    private int b;
    private int c;

    public Day_17(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_17>();
    }

    // This isn't so much an algorithm as an interpreter implementation. So, no time complexity per se.
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        a = int.Parse(lines[0].Split(": ")[1]);
        b = int.Parse(lines[1].Split(": ")[1]);
        c = int.Parse(lines[2].Split(": ")[1]);

        List<int> program = lines[4].Split(": ")[1].Split(",").Select(int.Parse).ToList();

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
                    outputs.Add(GetOperandValue(operand) % 8);
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

        _logger.LogInformation("Final output: {o}", string.Join(",", outputs));

        return long.Parse(string.Join("", outputs));
    }

    private int GetOperandValue(int operand)
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

    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }
}

using Microsoft.Extensions.Logging;

public class Day_3 : IDay
{
    private ILogger<Day_3> _logger;
    public Day_3(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_3>();
    }

    // Intuition: Reminiscent of compilers/translators.
    // Make a finite state machine to match the pattern - this can be done in O(N) time
    public int Solve(string inputPath)
    {
        string lines = string.Join("", File.ReadAllLines(inputPath));

        int total = EvaluateValidInstructions(lines);

        return total;
    }

    private enum State
    {
        STATE_BASE = 0,
        STATE_M,
        STATE_U,
        STATE_L,
        STATE_OPEN_PAREN,
        STATE_FIRST_NUM,
        STATE_COMMA,
        STATE_SECOND_NUM
    }

    private int EvaluateValidInstructions(string line)
    {
        int total = 0;
        State currentState = State.STATE_BASE;
        int firstNum = 0;
        int secondNum = 0;

        foreach (char c in line)
        {
            switch (currentState)
            {
                case State.STATE_BASE:
                    if (c == 'm')
                    {
                        currentState = State.STATE_M;
                    }
                    else
                    {
                        goto default;
                    }
                    break;
                case State.STATE_M:
                    if (c == 'u')
                    {
                        currentState = State.STATE_U;
                    }
                    else
                    {
                        goto default;
                    }
                    break;
                case State.STATE_U:
                    if (c == 'l')
                    {
                        currentState = State.STATE_L;
                    }
                    else
                    {
                        goto default;
                    }
                    break;
                case State.STATE_L:
                    if (c == '(')
                    {
                        currentState = State.STATE_OPEN_PAREN;
                    }
                    else
                    {
                        goto default;
                    }
                    break;
                case State.STATE_OPEN_PAREN:
                    if (char.IsNumber(c))
                    {
                        currentState = State.STATE_FIRST_NUM;
                        firstNum = firstNum * 10 + (int)(c - '0');
                    }
                    else
                    {
                        goto default;
                    }
                    break;
                case State.STATE_FIRST_NUM:
                    if (char.IsNumber(c))
                    {
                        currentState = State.STATE_FIRST_NUM;
                        firstNum = firstNum * 10 + (int)(c - '0');
                    }
                    else if (c == ',')
                    {
                        currentState = State.STATE_COMMA;
                    }
                    else
                    {
                        goto default;
                    }
                    break;
                case State.STATE_COMMA:
                    if (char.IsNumber(c))
                    {
                        currentState = State.STATE_SECOND_NUM;
                        secondNum = secondNum * 10 + (int)(c - '0');
                    }
                    else
                    {
                        goto default;
                    }
                    break;
                case State.STATE_SECOND_NUM:
                    if (char.IsNumber(c))
                    {
                        currentState = State.STATE_SECOND_NUM;
                        secondNum = secondNum * 10 + (int)(c - '0');
                    }
                    else if (c == ')')
                    {
                        // We have a complete instruction
                        total += firstNum * secondNum;
                        _logger.LogInformation("Found instruction {instruction}", $"mul({firstNum},{secondNum})");
                        goto default;
                    }
                    else
                    {
                        goto default;
                    }
                    break;
                default:
                    currentState = State.STATE_BASE;
                    firstNum = secondNum = 0;
                    break;
            }
        }

        return total;
    }

    // Intuition: O(N^2) method is to remove a single element at a time from a list, and then call IsReportSafe.
    // Not optimal; better would be to notice the first time a problem occurs, see if jumping (ignoring) it helps, and if not, marking as a failure
    public int SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);


        throw new NotImplementedException();
        // return safeReports;
    }

    private List<int> ParseLine(string line)
    {
        return line.Split(" ").Select(s => int.Parse(s)).ToList();
    }
}
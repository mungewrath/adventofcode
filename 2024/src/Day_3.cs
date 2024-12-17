
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
    public long Solve(string inputPath)
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
        STATE_SECOND_NUM,
        STATE_D,
        STATE_O,
        STATE_DO_OPEN,
        STATE_N,
        STATE_APOS,
        STATE_T,
        STATE_DONT_OPEN
    }

    // This could be better optimized by skipping ahead for known sequences like mul(, then you don't need intermediate states
    private int EvaluateValidInstructions(string line, bool useToggles = false)
    {
        int total = 0;
        State currentState = State.STATE_BASE;
        int firstNum = 0;
        int secondNum = 0;
        bool enabled = true;

        foreach (char c in line)
        {
            // Shortcut since you can get there from anywhere
            if (c == 'd' && useToggles)
            {
                currentState = State.STATE_D;
                firstNum = secondNum = 0;
                continue;
            }

            switch (currentState)
            {
                case State.STATE_BASE:
                    if (c == 'm' && enabled)
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
                case State.STATE_D:
                    if (c == 'o')
                    {
                        currentState = State.STATE_O;
                    }
                    else
                    {
                        goto default;
                    }
                    break;
                case State.STATE_O:
                    if (c == '(')
                    {
                        currentState = State.STATE_DO_OPEN;
                    }
                    else if (c == 'n')
                    {
                        currentState = State.STATE_N;
                    }
                    else
                    {
                        goto default;
                    }
                    break;
                case State.STATE_DO_OPEN:
                    if (c == ')')
                    {
                        enabled = true;
                    }
                    goto default;
                case State.STATE_N:
                    if (c == '\'')
                    {
                        currentState = State.STATE_APOS;
                    }
                    else
                    {
                        goto default;
                    }
                    break;
                case State.STATE_APOS:
                    if (c == 't')
                    {
                        currentState = State.STATE_T;
                    }
                    else
                    {
                        goto default;
                    }
                    break;
                case State.STATE_T:
                    if (c == '(')
                    {
                        currentState = State.STATE_DONT_OPEN;
                    }
                    else
                    {
                        goto default;
                    }
                    break;
                case State.STATE_DONT_OPEN:
                    if (c == ')')
                    {
                        enabled = false;
                    }
                    goto default;
                default:
                    currentState = State.STATE_BASE;
                    firstNum = secondNum = 0;
                    break;
            }
        }

        return total;
    }

    // Same as above, but the code was extended
    public long SolvePartTwo(string inputPath)
    {
        string lines = string.Join("", File.ReadAllLines(inputPath));

        int total = EvaluateValidInstructions(lines, useToggles: true);

        return total;
    }
}
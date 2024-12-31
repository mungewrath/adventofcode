using Microsoft.Extensions.Logging;

public class Day_21 : IDay
{
    private ILogger<Day_21> _logger;
    public Day_21(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_21>();
    }

    // Intuition: This has a lot of layers, but at its core it's translating button pushes in O(N) time.
    // Work backwards, starting from the robot pressing the numpad, to the robot controlling it, until we get to us at the top level.
    public long Solve(string inputPath)
    {
        string[] codes = File.ReadAllLines(inputPath);

        List<int> complexities = [];

        foreach (string code in codes)
        {
            var dpad1 = TranslateCodeToLevel1Robot(code);
            var dpad2 = TranslateDpadToDpad(dpad1);
            var human = TranslateDpadToDpad(dpad2);

            _logger.LogInformation("{dpad1}", dpad1);
            _logger.LogInformation("{dpad2}", dpad2);
            _logger.LogInformation("{human}", human);

            complexities.Add(human.Count * int.Parse(code[..^1]));
        }


        return complexities.Sum();
    }

    private List<char> TranslateCodeToLevel1Robot(string code)
    {
        Dictionary<char, (int x, int y)> coordsForButtons = new(){
            {'7', (0, 0) },
            {'8', (1, 0) },
            {'9', (2, 0) },
            {'4', (0, 1) },
            {'5', (1, 1) },
            {'6', (2, 1) },
            {'1', (0, 2) },
            {'2', (1, 2) },
            {'3', (2, 2) },
            {'0', (1, 3) },
            {'A', (2, 3) },
            {'X', (0, 3) } // Illegal spot
        };
        return Translate([.. code], coordsForButtons);
    }

    private static List<char> Translate(List<char> code, Dictionary<char, (int x, int y)> coordsForButtons)
    {
        List<char> dpad = [];
        (int x, int y) = coordsForButtons['A'];

        foreach (char c in code)
        {
            (int nx, int ny) = coordsForButtons[c];
            // Order is important so robot doesn't go over an empty space and "panic"
            List<char> stepsForNextButton = [];
            bool enteredIllegalSpot = false;

            while (nx < x)
            {
                stepsForNextButton.Add('<');
                x--;
                if ((x, y) == coordsForButtons['X'])
                {
                    enteredIllegalSpot = true;
                }
            }
            while (ny > y)
            {
                stepsForNextButton.Add('v');
                y++;
                if ((x, y) == coordsForButtons['X'])
                {
                    enteredIllegalSpot = true;
                }
            }
            while (ny < y)
            {
                stepsForNextButton.Add('^');
                y--;
                if ((x, y) == coordsForButtons['X'])
                {
                    enteredIllegalSpot = true;
                }
            }
            while (nx > x)
            {
                stepsForNextButton.Add('>');
                x++;
                if ((x, y) == coordsForButtons['X'])
                {
                    enteredIllegalSpot = true;
                }
            }

            if (enteredIllegalSpot)
            {
                stepsForNextButton.Reverse();
            }

            stepsForNextButton.Add('A');
            dpad.AddRange(stepsForNextButton);
        }

        return dpad;
    }

    private List<char> TranslateDpadToDpad(List<char> dpad1)
    {
        // List<char> dpad2 = [];
        Dictionary<char, (int x, int y)> coordsForButtons = new(){
            {'^', (1, 0) },
            {'A', (2, 0) },
            {'<', (0, 1) },
            {'v', (1, 1) },
            {'>', (2, 1) },
            {'X', (0, 0) }
        };

        return Translate(dpad1, coordsForButtons);
    }

    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }
}
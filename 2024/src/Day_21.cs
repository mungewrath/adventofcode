using Microsoft.Extensions.Logging;

public class Day_21 : IDay
{
    private ILogger<Day_21> _logger;
    private int _numLevels;
    private readonly Dictionary<char, (int x, int y)> _coordsForDpad = new(){
            {'^', (1, 0) },
            {'A', (2, 0) },
            {'<', (0, 1) },
            {'v', (1, 1) },
            {'>', (2, 1) },
            {'X', (0, 0) } // Illegal spot
        };
    private readonly Dictionary<char, (int x, int y)> _coordsForNumpad = new(){
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
    public Day_21(ILoggerFactory factory, int numLevels)
    {
        _logger = factory.CreateLogger<Day_21>();
        _numLevels = numLevels;
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

    private List<char> TranslateCodeToLevel1Robot(string code, char start = 'A')
    {
        return Translate([.. code], _coordsForNumpad, start);
    }

    private static List<char> Translate(List<char> code, Dictionary<char, (int x, int y)> coordsForButtons, char start)
    {
        List<char> dpad = [];
        (int x, int y) = coordsForButtons[start];

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

    private List<char> TranslateDpadToDpad(List<char> dpad1, char start = 'A')
    {
        return Translate(dpad1, _coordsForDpad, start);
    }

    private long TranslateDpadMemoized(List<char> code, int layers)
    {
        DefaultDict<(char prev, char next), long> counts = [];

        counts[('A', code[0])] = 1;
        for (int i = 1; i < code.Count; i++)
        {
            // if (!counts.ContainsKey((code[i - 1], code[i])))
            // {
            //     counts[(code[i - 1], code[i])] = 0;
            // }
            counts[(code[i - 1], code[i])] += 1;
        }

        for (int k = 0; k < layers; k++)
        {
            DefaultDict<(char prev, char next), long> nextCounts = [];

            foreach (var kv in counts)
            {
                List<char> translation = TranslateDpadToDpad([kv.Key.next], kv.Key.prev);

                // if (!nextCounts.ContainsKey(('A', translation[0])))
                // {
                //     nextCounts[('A', translation[0])] = 0;
                // }
                nextCounts[('A', translation[0])] += kv.Value;
                for (int i = 1; i < translation.Count; i++)
                {
                    // if (!nextCounts.ContainsKey((translation[i - 1], translation[i])))
                    // {
                    //     nextCounts[(translation[i - 1], translation[i])] = 0;
                    // }
                    nextCounts[(translation[i - 1], translation[i])] += kv.Value;
                }
            }

            counts = nextCounts;
        }

        return counts.Values.Sum();
    }

    // Intuition: The number of button presses grows exponentially with each layer of robots, so now it's O(N*2^R).
    // We can use memoization since the translation of a given set of arrows -> press A repeats layer by layer, and we only care about the total number of them.
    public long SolvePartTwo(string inputPath)
    {
        string[] codes = File.ReadAllLines(inputPath);

        List<long> complexities = [];

        foreach (string code in codes)
        {
            var dpad1 = TranslateCodeToLevel1Robot(code);
            var human = TranslateDpadMemoized(dpad1, _numLevels);

            _logger.LogInformation("First robot must press this on the dpad:{dpad1}", dpad1);
            _logger.LogInformation("human needs to press: {human} buttons", human);

            complexities.Add(human * long.Parse(code[..^1]));
        }


        return complexities.Sum();
    }
}
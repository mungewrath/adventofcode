// Intuition: Comparing first-smallest to first-smallest, second to second, etc, is saying "sort the lists".

using Microsoft.Extensions.Logging;

public class Day_1 : IDay
{
    private ILogger<Day_1> _logger;
    public Day_1(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_1>();
    }

    public int Solve(string inputPath)
    {
        (var firstList, var secondList) = ReadAllLines(inputPath);

        firstList.Sort();
        secondList.Sort();

        int sum = 0;

        for (int i = 0; i < firstList.Count; i++)
        {
            sum += Math.Abs(firstList[i] - secondList[i]);
        }

        return sum;
    }

    public int SolvePartTwo(string inputPath)
    {
        (var first, var second) = ReadAllLines(inputPath);

        var hashMap = new Dictionary<int, int>();

        foreach (int n in first)
        {
            hashMap[n] = 0;
        }

        foreach (int n in second)
        {
            if (hashMap.ContainsKey(n))
            {
                hashMap[n] = hashMap[n] + 1;
            }
        }

        return first.Select(n => n * (hashMap.ContainsKey(n) ? hashMap[n] : 0)).Sum();
    }

    private (List<int> first, List<int> second) ReadAllLines(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        List<int> firstList = [];
        List<int> secondList = [];

        foreach (string line in lines)
        {
            var nums = line.Split("   ");

            firstList.Add(int.Parse(nums[0]));
            secondList.Add(int.Parse(nums[1]));
        }

        return (firstList, secondList);
    }
}
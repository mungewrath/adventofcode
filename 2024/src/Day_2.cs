
using Microsoft.Extensions.Logging;

public class Day_2 : IDay
{
    private ILogger<Day_2> _logger;
    public Day_2(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_2>();
    }

    // Intuition: Each line is traversing and remembering something from the prior element.
    // You can do this a couple memory variables in O(N) time and constant space.
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        int safeReports = 0;

        foreach (string line in lines)
        {
            var nums = ParseLine(line);

            _logger.LogInformation($"Parsing report {line}");
            if (IsReportSafe(nums))
                safeReports++;
        }

        return safeReports;
    }

    private bool IsReportSafe(List<int> nums)
    {

        if (nums.Count < 2)
            return true;

        int sign = Math.Sign(nums[1] - nums[0]);

        for (int i = 1; i < nums.Count; i++)
        {
            if (Math.Sign(nums[i] - nums[i - 1]) != sign)
            {
                _logger.LogWarning($"Elements {i} and {i - 1} didn't match the increasing/decreasing pattern. Marking unsafe");
                return false;
            }

            if (Math.Abs(nums[i] - nums[i - 1]) > 3 || Math.Abs(nums[i] - nums[i - 1]) < 1)
            {
                _logger.LogWarning($"Elements {i} and {i - 1} had a difference of {Math.Abs(nums[i] - nums[i - 1])}. Marking unsafe");
                return false;
            }
        }

        _logger.LogInformation("Marking report safe!");

        return true;
    }

    // Intuition: O(N^2) method is to remove a single element at a time from a list, and then call IsReportSafe.
    // Not optimal; better would be to notice the first time a problem occurs, see if jumping (ignoring) it helps, and if not, marking as a failure
    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        int safeReports = 0;

        foreach (string line in lines)
        {
            var nums = ParseLine(line);

            _logger.LogInformation($"Parsing report {line}");

            if (IsReportSafe(nums))
            {
                safeReports++;
                continue;
            }

            _logger.LogInformation("Unmodified report is unsafe, so trying out the stabilizer");

            bool stabilizerFound = false;

            for (int i = 0; i < nums.Count; i++)
            {

                if (IsReportSafe([.. nums[..i], .. nums[(i + 1)..nums.Count]]))
                {
                    safeReports++;
                    _logger.LogInformation($"Found a match by excluding element at index {i}");
                    stabilizerFound = true;
                    break;
                }
            }

            if (!stabilizerFound)
            {
                _logger.LogWarning("No possible safe match found");
            }
        }

        return safeReports;
    }

    private List<int> ParseLine(string line)
    {
        return line.Split(" ").Select(s => int.Parse(s)).ToList();
    }
}
using System.Text;
using Microsoft.Extensions.Logging;
using Serilog.Sinks.File;

public class Day_9 : IDay
{
    private ILogger<Day_9> _logger;
    public Day_9(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_9>();
    }

    // Intuition: This can be solved using two pointers, one starting at the front and the other working from the back.
    // This will complete in O(N) time.
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        int[] disk = ReadDiskContents(lines.Single());

        _logger.LogInformation("Read disk of size {size}", disk.Length);
        _logger.LogInformation("Disk contents: {disk}", PrintDisk(disk));

        int emptyIdx = 0;
        int compactorIdx = disk.Length - 1;

        while (emptyIdx < compactorIdx)
        {
            if (disk[emptyIdx] != -1)
            {
                emptyIdx++;
                continue;
            }
            if (disk[compactorIdx] == -1)
            {
                compactorIdx--;
                continue;
            }

            disk[emptyIdx++] = disk[compactorIdx];
            disk[compactorIdx--] = -1;
        }

        return ComputeChecksum(disk);
    }

    private static long ComputeChecksum(int[] disk)
    {
        long checksum = 0;
        int idx = 0;
        while (idx < disk.Length)
        {
            if (disk[idx] == -1)
            {
                idx++;
                continue;
            }

            checksum += idx * disk[idx];
            idx++;
        }

        return checksum;
    }

    private int[] ReadDiskContents(string line)
    {
        long totalLength = line.Sum(c => c - '0');
        int[] disk = new int[totalLength];
        int idx = 0;
        int fileId = 0;

        bool readingFile = true;


        foreach (char c in line)
        {
            int size = c - '0';
            if (readingFile)
            {
                for (int i = 0; i < size; i++)
                {
                    disk[i + idx] = fileId;
                }
                fileId++;
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    disk[i + idx] = -1;
                }
            }

            readingFile = !readingFile;
            idx += size;
        }

        // Fill the rest of the disk with empty
        while (idx < disk.Length)
        {
            disk[idx++] = -1;
        }

        return disk;
    }

    private string PrintDisk(int[] disk)
    {
        StringBuilder sb = new();

        foreach (int i in disk)
        {
            if (i == -1)
            {
                sb.Append('.');
            }
            else
            {
                sb.Append(i);
            }
        }

        return sb.ToString();
    }

    // Intuition: Brute force way would be to scan for a contiguous block of space for every file, O(N^2).
    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        int[] disk = ReadDiskContents(lines.Single());

        _logger.LogInformation("Read disk of size {size}", disk.Length);
        _logger.LogInformation("Disk contents: {disk}", PrintDisk(disk));

        int compactorIdx = disk.Length - 1;

        while (compactorIdx > 0)
        {
            if (disk[compactorIdx] == -1)
            {
                compactorIdx--;
                continue;
            }

            int fileEnd = compactorIdx;
            int fileId = disk[compactorIdx];
            int fileStart = compactorIdx;

            while (fileStart > 0 && disk[fileStart - 1] == fileId)
            {
                fileStart--;
            }

            _logger.LogInformation("Identified next file to move as {s}:{e}", fileStart, fileEnd);

            int fileSize = fileEnd - fileStart + 1;
            int emptyIdx = 0;

            while (emptyIdx < fileStart)
            {
                while (disk[emptyIdx] != -1 && emptyIdx < fileStart)
                {
                    emptyIdx++;
                }

                if (EmptySpaceIsLargeEnough(emptyIdx, fileSize, disk))
                {
                    _logger.LogInformation("Found empty space at {e}, swapping", emptyIdx);
                    SwapFileWithEmptySpace(emptyIdx, fileStart, fileSize, disk);
                    // _logger.LogInformation("=== {disk}", PrintDisk(disk));
                    break;
                }
                else
                {
                    // _logger.LogInformation("Found empty space at {e}, but it wasn't large enough. Skipping ahead", emptyIdx);
                    while (disk[emptyIdx] == -1 && emptyIdx < fileStart)
                    {
                        emptyIdx++;

                    }
                }
            }

            while (compactorIdx > 0 && disk[compactorIdx] == fileId)
                compactorIdx--;
        }

        return ComputeChecksum(disk);
    }

    private bool EmptySpaceIsLargeEnough(int start, int fileSize, int[] disk)
    {
        for (int i = 0; i < fileSize; i++)
        {
            if (disk[start + i] != -1)
            {
                return false;
            }
        }
        return true;
    }

    private void SwapFileWithEmptySpace(int emptyIdx, int fileStart, int fileSize, int[] disk)
    {
        for (int i = 0; i < fileSize; i++)
        {
            disk[emptyIdx + i] = disk[fileStart + i];
            disk[fileStart + i] = -1;
        }
    }
}
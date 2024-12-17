using Microsoft.Extensions.Logging;

public class Day_8 : IDay
{
    private ILogger<Day_8> _logger;
    public Day_8(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_8>();
    }

    private HashSet<(int, int)> hotSpots = [];
    private int mapHeight = 0;
    private int mapWidth = 0;


    // Intuition: The pattern for computing hot spots is closed-form, so we can list out all antennas of every type and map the combinations.
    // O(N*A^2), where N is the number of antennas and A is the number of antennas in a pattern.
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);
        var allAntennas = ReadMap(lines);
        mapHeight = lines.Length;
        mapWidth = lines[0].Length;

        foreach (var codeAndAntennas in allAntennas)
        {
            char code = codeAndAntennas.Key;

            _logger.LogInformation("Identifying hotspots for antenna type {c}", code);
            List<Antenna> antennas = codeAndAntennas.Value;

            for (int i = 0; i < antennas.Count; i++)
            {
                for (int j = i + 1; j < antennas.Count; j++)
                {
                    (int dx, int dy) = antennas[i].CalculateDistance(antennas[j]);

                    // x1+dx, if not x1
                    if (antennas[i].X + dx != antennas[j].X && antennas[i].Y + dy != antennas[j].Y)
                    {
                        AddHotspotIfOnMap(antennas[i].X + dx, antennas[i].Y + dy);
                    }
                    // x1-dx
                    if (antennas[i].X - dx != antennas[j].X && antennas[i].Y - dy != antennas[j].Y)
                    {
                        AddHotspotIfOnMap(antennas[i].X - dx, antennas[i].Y - dy);
                    }
                    // x2+dx
                    if (antennas[j].X + dx != antennas[i].X && antennas[j].Y + dy != antennas[i].Y)
                    {
                        AddHotspotIfOnMap(antennas[j].X + dx, antennas[j].Y + dy);
                    }
                    // x2-dx
                    if (antennas[j].X - dx != antennas[i].X && antennas[j].Y - dy != antennas[i].Y)
                    {
                        AddHotspotIfOnMap(antennas[j].X - dx, antennas[j].Y - dy);
                    }
                }
            }
        }

        return hotSpots.Count;
    }

    private void AddHotspotIfOnMap(int x, int y)
    {
        if (x < 0 || x >= mapWidth ||
           y < 0 || y >= mapHeight)
        {
            return;
        }
        else
        {
            hotSpots.Add((x, y));
        }
    }

    private Dictionary<char, List<Antenna>> ReadMap(string[] lines)
    {
        Dictionary<char, List<Antenna>> antennas = [];

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                char c = lines[y][x];

                if (c == '.')
                {
                    continue;
                }

                if (!antennas.ContainsKey(c))
                {
                    antennas[c] = [];
                }
                antennas[c].Add(new()
                {
                    X = x,
                    Y = y
                });
            }
        }

        return antennas;
    }

    private class Antenna
    {
        public int X;
        public int Y;
        public (int, int) CalculateDistance(Antenna other)
        {
            return (X - other.X, Y - other.Y);
        }
    }

    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }
}
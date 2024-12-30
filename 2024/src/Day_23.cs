using System.Collections;
using Microsoft.Extensions.Logging;

public class Day_23 : IDay
{
    private ILogger<Day_23> _logger;
    public Day_23(ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<Day_23>();
    }

    // Intuition: This is asking us to find complete subgraphs, which is a case of the clique problem.
    // General clique problem is NP complete, so to just solve for the specific case of 3, iterate through the nodes
    // and find two neighbors that connect to each other and the first node in O(N^3) time.
    public long Solve(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        Dictionary<string, LanNode> nodes = [];

        foreach (string line in lines)
        {
            var nodeNames = line.Split("-");
            if (!nodes.TryGetValue(nodeNames[0], out LanNode a))
            {
                a = new LanNode(nodeNames[0]);
                nodes[nodeNames[0]] = a;
            }

            if (!nodes.TryGetValue(nodeNames[1], out LanNode b))
            {
                b = new LanNode(nodeNames[1]);
                nodes[nodeNames[1]] = b;
            }

            a.Connections.Add(b.Name, b);
            b.Connections.Add(a.Name, a);
        }

        long setsOfThree = 0;
        // List<string>

        var sorted = nodes.Values.ToList()
            .Where(n => n.Name[0] == 't')
            .OrderBy(n => n.Name, new GraphNameComparer());

        foreach (LanNode n1 in sorted)
        {
            var comp = new GraphNameComparer();
            var neighbors = n1.Connections.Values
                .Where(n2 => comp.Compare(n1.Name, n2.Name) < 0)
                .ToList();
            for (int i = 0; i < neighbors.Count; i++)
            {
                var n2 = neighbors[i];
                foreach (var n3 in n2.Connections.Where(c => comp.Compare(n2.Name, c.Key) < 0))
                {
                    if (n1.Connections.ContainsKey(n3.Key))
                    {
                        // Found a match
                        setsOfThree++;
                        _logger.LogInformation("Found a set of three: {n1}-{n2}-{n3}", n1.Name, n2.Name, n3.Key);
                    }
                }
            }
        }

        return setsOfThree;
    }

    private class LanNode(string name)
    {
        public readonly string Name = name;
        public readonly Dictionary<string, LanNode> Connections = [];
    }

    private class GraphNameComparer : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            for (int idx = 0; idx < 2; idx++)
            {
                var r = Compare(x[idx], y[idx]);
                if (r != 0)
                {
                    return r;
                }
            }

            return 0;
        }

        private int Compare(char x, char y)
        {
            if (x == y)
            {
                return 0;
            }
            else if (x == 't')
            {
                return -1;
            }
            else if (y == 't')
            {
                return 1;
            }
            else
            {
                return x - y;
            }
        }
    }

    public long SolvePartTwo(string inputPath)
    {
        string[] lines = File.ReadAllLines(inputPath);

        return 0;
    }
}
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
        Dictionary<string, LanNode> nodes = ReadGraph(inputPath);

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

    private static Dictionary<string, LanNode> ReadGraph(string inputPath)
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

        return nodes;
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

    // Intuition: Now we use Bron-Kerbosch algorithm to find the largest clique.
    // For a sparse graph, the time complexity is quick in practice.
    public long SolvePartTwo(string inputPath)
    {
        Dictionary<string, LanNode> nodes = ReadGraph(inputPath);

        List<List<LanNode>> cliques = BronKerbosch([], [.. nodes.Values], [], []);

        long largestCliqueSize = 0;
        List<LanNode> lanParty = null;

        foreach (var clq in cliques)
        {
            if (clq.Count > largestCliqueSize)
            {
                lanParty = clq;
                largestCliqueSize = clq.Count;
            }
        }

        string password = string.Join(",", lanParty.OrderBy(ln => ln.Name).Select(ln => ln.Name));

        _logger.LogInformation("The password is {password}", password);

        return 0;
    }

    /// <summary>
    /// Finds all maximal cliques in a graph using the Bron-Kerbosch algorithm.
    /// </summary>
    /// <param name="clique">The current clique being built.</param>
    /// <param name="possible">The list of nodes that can be added to the current clique.</param>
    /// <param name="excluded">The list of nodes that have been excluded from the current clique.</param>
    /// <param name="allCliques">The list of all maximal cliques found so far.</param>
    /// <returns>A list of all maximal cliques in the graph.</returns>
    private List<List<LanNode>> BronKerbosch(List<LanNode> clique, List<LanNode> possible, List<LanNode> excluded, List<List<LanNode>> allCliques)
    {
        if (possible.Count == 0 && excluded.Count == 0)
        {
            allCliques.Add(clique);
            return allCliques;
        }

        while (possible.Count > 0)
        {
            var v = possible[possible.Count - 1];

            BronKerbosch(
                clique: [.. clique, v],
                possible: possible.Where(p => v.Connections.ContainsKey(p.Name)).ToList(),
                excluded: excluded.Where(x => v.Connections.ContainsKey(x.Name)).ToList(),
                allCliques
                );

            possible.RemoveAt(possible.Count - 1);
            excluded.Add(v);
        }

        return allCliques;
    }
}
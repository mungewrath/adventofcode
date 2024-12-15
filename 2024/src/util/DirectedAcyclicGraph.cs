using Microsoft.Extensions.Logging;

public class DirectedAcyclicGraph
{
    private Dictionary<int, Node> _nodes;
    private ILogger<DirectedAcyclicGraph> _logger;

    public DirectedAcyclicGraph(Dictionary<int, List<int>> dependencies, List<int> nodeIds, ILoggerFactory factory)
    {
        _logger = factory.CreateLogger<DirectedAcyclicGraph>();

        if (nodeIds.Distinct().Count() != nodeIds.Count)
        {
            throw new ArgumentException("List of node IDs has duplicates");
        }

        var allNodesInGraph = nodeIds.ToHashSet();

        _nodes = [];

        foreach (int id in nodeIds)
        {
            _nodes.Add(id, new Node(id));
        }
        foreach (int id in nodeIds)
        {
            if (dependencies.ContainsKey(id))
            {
                foreach (int prior in dependencies[id])
                {
                    if (allNodesInGraph.Contains(prior))
                    {
                        _nodes[prior].AddEdge(id);
                    }
                }
            }
        }
    }

    // Assumes no cycle present
    public List<int> TopographicalSort()
    {
        LinkedList<int> sorted = [];

        foreach (Node n in _nodes.Values)
        {
            Visit(n, sorted);
        }

        return [.. sorted];
    }

    private void Visit(Node n, LinkedList<int> sorted)
    {
        if (n.Visited)
        {
            _logger.LogInformation("{nodeId} has already been visited. Returning.", n.Id);
            return;
        }
        foreach (int descendantId in n.OutgoingEdges)
        {
            _logger.LogInformation("Visiting child in DAG {descendantId}", descendantId);
            Visit(_nodes[descendantId], sorted);
        }

        _logger.LogInformation("Marking {nodeId} as visited and adding to list", n.Id);
        n.Visited = true;
        sorted.AddFirst(n.Id);
    }

    protected class Node
    {
        public Node(int id)
        {
            Id = id;
        }

        public void AddEdge(int destId)
        {
            OutgoingEdges.Add(destId);
        }

        public int Id;
        public bool Visited = false;
        public List<int> OutgoingEdges = [];
    }
}
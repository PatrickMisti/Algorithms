namespace AllKruskal;

internal class Calculation
{
    public static List<List<Edge>> Run(List<Edge> edges)
    {
        var pq = new PriorityQueue<Edge, Edge>(edges.Select(e => (e, e)));
        var allResults = new List<List<Edge>>();
        var nodes = Fill(edges);

        var parent = MakeInitialParent(nodes);

        KruskalRecursive(pq, new List<Edge>(), parent, allResults, nodes.Count - 1);

        return allResults;
    }

    private static List<Node> Fill(List<Edge> edges)
    {
        var nodes = new HashSet<Node>();

        foreach (var edge in edges)
        {
            nodes.Add(edge.From);
            nodes.Add(edge.To);
        }

        return nodes.ToList();
    }

    private static Dictionary<Node, Node> MakeInitialParent(List<Node> nodes)
    {
        var parent = new Dictionary<Node, Node>();
        foreach (var node in nodes)
            parent[node] = node;
        return parent;
    }

    private static Node Find(Node node, Dictionary<Node, Node> parent)
    {
        if (parent[node] != node)
            parent[node] = Find(parent[node], parent);
        return parent[node];
    }

    private static void Bind(Node a, Node b, Dictionary<Node, Node> parent)
    {
        var rootA = Find(a, parent);
        var rootB = Find(b, parent);
        if (rootA != rootB)
            parent[rootA] = rootB;
    }

    private static bool IsCycle(Node node1, Node node2, Dictionary<Node, Node> parent)
    {
        var rootA = Find(node1, parent);
        var rootB = Find(node2, parent);
        return rootA == rootB;
    }

    private static void KruskalRecursive(
        PriorityQueue<Edge, Edge> edges,
        List<Edge> current,
        Dictionary<Node, Node> parent,
        List<List<Edge>> result,
        int targetEdgeCount)
    {
        if (current.Count == targetEdgeCount)
        {
            result.Add(new (current));
            return;
        }

        var edgeSnapshot = edges.UnorderedItems.ToList();

        for (int i = 0; i < edgeSnapshot.Count; i++)
        {
            var edge = edgeSnapshot[i].Element;
            var remaining = new PriorityQueue<Edge, Edge>(edgeSnapshot.Skip(i + 1).Select(x => (x.Element, x.Priority)));

            if (IsCycle(edge.From, edge.To, parent)) continue;

            var newParent = new Dictionary<Node, Node>(parent);
            Bind(edge.From, edge.To, newParent);

            current.Add(edge);
            KruskalRecursive(remaining, current, newParent, result, targetEdgeCount);
            current.Remove(current.Last()); // Backtrack
        }
    }
}
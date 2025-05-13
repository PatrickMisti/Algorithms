namespace AllKruskal;

internal class Calculation
{
    public static List<List<Edge>> Run(List<Edge> edges)
    {
        var allResults = new List<List<Edge>>();
        var parent = MakeInitialParent(Fill(edges));
        edges.Sort();

        KruskalRecursive(edges, new List<Edge>(), parent, ref allResults, parent.Count - 1);

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

    private static void Bind(Node node1, Node node2, ref Dictionary<Node, Node> parent)
    {
        var root1 = Find(node1, ref parent);
        var root2 = Find(node2, ref parent);
        if (root1 != root2)
            parent[root1] = root2;
    }

    private static Node Find(Node node, ref Dictionary<Node, Node> parent)
    {
        if (parent[node] != node)
            parent[node] = Find(parent[node], ref parent);
        return parent[node];
    }

    private static bool IsCycle(Node node1, Node node2, ref Dictionary<Node, Node> parent) 
        => Find(node1, ref parent) == Find(node2, ref parent);
    

    private static void KruskalRecursive(
        List<Edge> edges,
        List<Edge> result,
        Dictionary<Node, Node> parent,
        ref List<List<Edge>> allResult,
        int targetEdgeCount)
    {
        if (result.Count == targetEdgeCount)
        {
            allResult.Add([.. result]);
            return;
        }

        for (int i = 0; i < edges.Count; i++)
        {
            var edge = edges[i];
            var remaining = edges.Skip(i + 1).ToList();

            if (IsCycle(edge.From, edge.To, ref parent)) continue;

            var newParent = new Dictionary<Node, Node>(parent);
            Bind(edge.From, edge.To, ref newParent);

            result.Add(edge);
            KruskalRecursive(remaining, result, newParent, ref allResult, targetEdgeCount);
            result.Remove(result.Last()); // Backtrack
        }
    }
}
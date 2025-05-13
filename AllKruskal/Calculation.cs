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

    public static List<Node> Fill(List<Edge> edges)
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

        foreach (var edge in edges)
        {
            var remaining = edges.Skip(edges.IndexOf(edge) + 1).ToList();

            if (IsCycle(edge.From, edge.To, ref parent)) continue;

            var newParent = new Dictionary<Node, Node>(parent);
            Bind(edge.From, edge.To, ref newParent);

            KruskalRecursive(remaining, [..result, edge], newParent, ref allResult, targetEdgeCount);
        }
    }


    private static List<Edge>? FindPath(Node start, Node end, List<Edge> mst, HashSet<Node> visited)
    {
        if (start == end)
            return [];

        visited.Add(start);
        var list = mst.Where(e => e.To.Equals(start) || e.From.Equals(start));

        foreach (var edge in list)
        {
            var next = edge.To.Equals(start) ? edge.From : edge.To;
            if (visited.Contains(next)) continue;

            var path = FindPath(next, end, mst, visited);
            if (path != null)
                return [.. path, edge];
        }

        return null;
    }

    public static bool IsMstUnique(List<Node> nodes, List<Edge> edges,List<Edge> mst)
    {
        var mstHash = mst.ToHashSet();

        foreach (var edge in edges)
        {
            if (mstHash.Contains(edge)) continue;

            var path = FindPath(edge.From, edge.To, mst, new HashSet<Node>());
            if (path == null) continue;

            var max = path.Max(e => e.Cost);

            if (edge.Cost == max) 
                return false;
        }

        return true;
    }
}
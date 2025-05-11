namespace AllKruskal;

internal class Calculation
{
    private static readonly Dictionary<Node, Node> Parent = new();

    public static List<HashSet<Edge>> Calculate(List<Edge> list) 
    {
        var result = new List<HashSet<Edge>>();

        Fill(list);
        var b = new PriorityQueue<Edge, Edge>(list.Select(s => (s, s)));     // B
        var s = new HashSet<Edge>();                                             // S

        while (b.TryDequeue(out var current, out _) && s.Count != list.Count - 1)
        {
            Print(b, s, current);
            if (IsCycle(current.From, current.To)) continue;
            s.Add(current); 
            Bind(current.From, current.To);
        }

        return result;
    }

    private static void Print(PriorityQueue<Edge, Edge> b, HashSet<Edge> s, Edge e)
    {
        Console.WriteLine($"B  : [{string.Join(",", b.UnorderedItems.Select(o => o.Element))}]");
        Console.WriteLine($"S  : [{string.Join(",", s)}]");
        Console.WriteLine($"e* : {e}");
        Console.WriteLine("----------------------------------------------\n");
    }

    private static void Fill(List<Edge> edges)
    {
        foreach (var edge in edges)
        {
            Parent[edge.From] = edge.From;
            Parent[edge.To] = edge.To;
        }
    }

    private static void Bind(Node node1, Node node2)
    {
        var root1 = Find(node1);
        var root2 = Find(node2);
        if (root1 != root2)
            Parent[root1] = root2;
    }

    private static Node Find(Node node)
    {
        if (Parent[node] != node)
            Parent[node] = Find(Parent[node]);
        return Parent[node];
    }

    private static bool IsCycle(Node node1, Node node2)
    {
        return Find(node1) == Find(node2);
    }
}
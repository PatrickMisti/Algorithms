namespace MstHeuristicTsp;

internal class Calculation
{
    private static readonly Dictionary<Node, Node> Parent = new();

    public static HashSet<Edge> Calculate(List<Edge> list) 
    {
        Fill(list);
        var b = CreatePriorityQueue(list);                           // B
        var s = new HashSet<Edge>();                                             // S

        while (b.TryDequeue(out var current, out _) && s.Count != list.Count - 1)
        {
            Print(b, s, current);
            if (IsCycle(current.From, current.To)) continue;
            s.Add(current); 
            Bind(current.From, current.To);
        }

        return s;
    }

    private static PriorityQueue<Edge, Edge> CreatePriorityQueue(List<Edge> list)
    {
        var b = new PriorityQueue<Edge, Edge>();
        var check = new HashSet<int>();
        foreach (var edge in list)
        {
            if (check.Add(edge.GetHashCode()))
                b.Enqueue(edge, edge);
        }
        return b;
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

    public static void CombineNodeWithMst(ref List<Node> nodes, HashSet<Edge> mst)
    {
        foreach (var edge in mst)
        {
            var toIndex = nodes.IndexOf(edge.From);
            nodes[toIndex].AddEdge(edge.To, edge.Cost);
            var fromIndex = nodes.IndexOf(edge.To);
            nodes[fromIndex].AddEdge(edge.From, edge.Cost);
        }
    }

    public static Dictionary<Node, List<Node>> CreateDictionary(List<Node> nodes)
    {
        var dict = new Dictionary<Node, List<Node>>();
        foreach (var node in nodes)
        {
            dict[node] = new List<Node>();
            foreach (var edge in node.GetEdges())
            {
                dict[node].Add(edge.From);
            }
        }
        return dict;
    }

    public static List<Node>? CreateEulerKreis(List<Node> nodes)
    {
        var localGraph = Calculation.CreateDictionary(nodes);
        var eulerPath = new List<Node>();

        void Hierholzer(Node node)
        {
            while (localGraph[node].Count > 0)
            {
                var next = localGraph[node][0];
                localGraph[node].Remove(next);
                localGraph[next].Remove(node);
                Hierholzer(next);
            }
            eulerPath.Add(node);
        }

        var start = nodes.Find(x => x.Name == "Linz");
        if (start == null)
        {
            Console.WriteLine("Start node not found");
            return null;
        }
        Hierholzer(start);
        eulerPath.Reverse();
        return eulerPath;
    }

    public static List<Node>? CreateHamiltonKreis(List<Node>? eulerPath)
    {
        if (eulerPath == null)
            return null;
        var visited = new HashSet<Node>();
        var hamiltonPath = new List<Node>();

        foreach (var node in eulerPath)
        {
            if (visited.Add(node))
                hamiltonPath.Add(node);
        }

        return hamiltonPath;
    }
}
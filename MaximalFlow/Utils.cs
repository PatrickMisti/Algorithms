namespace MaximalFlow;

internal class Utils
{
    public static void AddEdge(ref Node from, ref Node to, int capacity)
    {
        Edge forwardEdge = new Edge { To = to, Capacity = capacity, Flow = 0 };
        Edge reverseEdge = new Edge { To = from, Capacity = 0, Flow = 0 };
        forwardEdge.Reverse = reverseEdge;
        reverseEdge.Reverse = forwardEdge;
        from.Edges.Add(forwardEdge);
        to.Edges.Add(reverseEdge);
    }

    public static int MaxFlow(Node source, Node sink, List<Node> nodes)
    {
        int totalFlow = 0;
        while (true)
        {
            var path = FindAugmentingPath(source, sink, nodes);
            if (path == null || path.Count == 0)
                break;

            int flow = int.MaxValue;
            foreach (var edge in path)
                flow = Math.Min(flow, edge.ResidualCapacity);
            
            foreach (var edge in path)
            {
                edge.Flow += flow;
                edge.Reverse.Flow -= flow;
            }

            Console.WriteLine($"Augmenting path with flow = {flow}");
            Console.WriteLine("Path: " + string.Join(" -> ", path.Select(e => e.Reverse.To.Name + "->" + e.To.Name)));

            totalFlow += flow;
        }
        return totalFlow;
    }

    public static List<Edge>? FindAugmentingPath(Node source, Node sink, List<Node> nodes)
    {
        var visited = new HashSet<Node>();
        var queue = new Queue<Node>();
        var parentMap = new Dictionary<Node, Edge>();

        queue.Enqueue(source);
        visited.Add(source);
        
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current == sink)
            {
                var path = new List<Edge>();
                while (parentMap.ContainsKey(current))
                {
                    var edge = parentMap[current];
                    path.Add(edge);
                    current = edge.Reverse.To;
                }
                path.Reverse();
                return path;
            }
            foreach (var edge in current.Edges)
            {
                if (!visited.Contains(edge.To) && edge.ResidualCapacity > 0)
                {
                    visited.Add(edge.To);
                    queue.Enqueue(edge.To);
                    parentMap[edge.To] = edge;
                }
            }
        }
        return null;
    }
}
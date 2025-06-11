namespace MinCut;

internal class Calculator
{
    private Dictionary<string, List<string>> adj;
    // Restkapazität für jede Kante
    private Dictionary<string, Dictionary<string, int>> capacity;

    public Calculator(List<Edge> edges)
    {
        adj = new Dictionary<string, List<string>>();
        capacity = new Dictionary<string, Dictionary<string, int>>();

        foreach (var edge in edges)
        {
            string u = edge.From;
            string v = edge.To;
            int cap = edge.Weight;

            // Init Adjacency
            if (!adj.ContainsKey(u)) adj[u] = new List<string>();
            if (!adj.ContainsKey(v)) adj[v] = new List<string>();
            if (!adj[u].Contains(v)) adj[u].Add(v);
            if (!adj[v].Contains(u)) adj[v].Add(u);

            // Init Capacity
            if (!capacity.ContainsKey(u)) capacity[u] = new Dictionary<string, int>();
            if (!capacity.ContainsKey(v)) capacity[v] = new Dictionary<string, int>();

            if (!capacity[u].ContainsKey(v)) capacity[u][v] = 0;
            if (!capacity[v].ContainsKey(u)) capacity[v][u] = 0;

            // both directions for undirected graph
            capacity[u][v] += cap;
            capacity[v][u] += cap;
        }
    }

    public int MaxFlow(string source, string sink)
    {
        var flow = 0;
        var parent = new Dictionary<string, string>();

        while (BFS(source, sink, parent))
        {
            int pFlow = int.MaxValue;
            string cur = sink;

            while (cur != source)
            {
                string prev = parent[cur];
                pFlow = Math.Min(pFlow, capacity[prev][cur]);
                cur = prev;
            }

            cur = sink;
            while (cur != source)
            {
                string prev = parent[cur];
                capacity[prev][cur] -= pFlow;
                capacity[cur][prev] += pFlow;
                cur = prev;
            }

            flow += pFlow;
        }

        return flow;
    }

    private bool BFS(string source, string sink, Dictionary<string, string> parent)
    {
        parent.Clear();
        var visited = new HashSet<string>();
        var queue = new Queue<string>();
        queue.Enqueue(source);
        visited.Add(source);

        while (queue.Count > 0)
        {
            string u = queue.Dequeue();

            foreach (var v in adj[u])
            {
                if (!visited.Contains(v) && capacity[u][v] > 0)
                {
                    visited.Add(v);
                    parent[v] = u;
                    if (v == sink)
                        return true;
                    queue.Enqueue(v);
                }
            }
        }

        return false;
    }

    public List<Edge> GetMinCut(string source)
    {
        var visited = new HashSet<string>();
        var queue = new Queue<string>();
        queue.Enqueue(source);
        visited.Add(source);

        // BFS, to find reachable nodes from source
        while (queue.Count > 0)
        {
            var u = queue.Dequeue();
            foreach (var v in adj[u])
            {
                if (!visited.Contains(v) && capacity[u][v] > 0)
                {
                    visited.Add(v);
                    queue.Enqueue(v);
                }
            }
        }

        var cutEdges = new List<Edge>();

        foreach (var u in visited)
        {
            foreach (var v in adj[u])
            {
                if (!visited.Contains(v) && capacity.ContainsKey(u) && capacity[u].ContainsKey(v))
                {
                    int originalWeight = capacity[v][u];
                    cutEdges.Add(new Edge { From = u, To = v, Weight = originalWeight });
                }
            }
        }

        return cutEdges;
    }
}
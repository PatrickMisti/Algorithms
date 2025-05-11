namespace SpannbaumKruskal;

internal class Calculation
{
    public static List<Edge> Calculate(List<Edge> edges)
    {
        var result = new List<Edge>(); // S

        var b = new PriorityQueue<Edge, Edge>(); // B

        foreach (var edge in edges)
            b.Enqueue(edge, edge);

        while (b.Count > 0)
        {
            var current = b.Dequeue(); // e*

            var from = current.From;
            var to = current.To;

            var fromSet = FindEdges()
        }

        return result;
    }

    static List<Edge> FindEdges(List<Edge> s,Node search, Edge e) 
        => (from edge in s 
                where edge.IsNodeInEdge(search) && !edge.Equals(e) 
                select edge)
            .ToList();
    
}
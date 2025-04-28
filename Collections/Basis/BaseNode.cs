using Collections.AStar;

namespace Collections.Basis;

public class BaseNode
{
    public string Name { get; set; } = string.Empty;
    public BaseNode Parent { get; set; } = null!;
    public int Cost { get; set; } = int.MaxValue;
    public bool IsVisited { get; set; } = false;
    public IList<Edge> Edges { get; set; } = new List<Edge>();

    public BaseNode()
    {
        
    }

    public BaseNode(string name)
    {
        Name = name;
    }

    public void AddEdge(Node node, int cost)
    {
        if (Edges.Any(e => e.To == node)) return;
        Edges.Add(new Edge(cost, from: this, to: node));
    }

    public bool RemoveEdge(Node node)
    {
        var item = Edges
            .ToList()
            .Find(opt => opt.From != null && opt.From.Equals(node));
        if (item == null) return false;
        return Edges.Remove(item);
    }
}
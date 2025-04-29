namespace Collections.Basis;

public abstract class BaseNode
{
    public string Name { get; set; } = string.Empty;
    public BaseNode Parent { get; set; } = null!;
    public double Cost { get; set; } = double.MaxValue;
    public bool IsVisited { get; set; } = false;
    public IList<Edge> Edges { get; set; } = new List<Edge>();

    protected BaseNode()
    {
        
    }

    protected BaseNode(string name)
    {
        Name = name;
    }

    public void AddEdge(BaseNode node, int cost)
    {
        if (Edges.Any(e => e.To == node)) return;
        Edges.Add(new Edge(cost, from: this, to: node));
    }

    public void AddBiEdge(BaseNode first, BaseNode second, int cost)
    {
        AddEdge(second, cost);
        second.AddEdge(this, cost);
    }

    public bool RemoveEdge(BaseNode node)
    {
        var item = Edges
            .ToList()
            .Find(opt => opt.From != null && opt.From.Equals(node));

        if (item == null) return false;
        return Edges.Remove(item);
    }

    public abstract bool IsValid();
}
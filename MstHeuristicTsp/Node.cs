namespace MstHeuristicTsp;

internal class Node(string name) : IComparable<Node>
{
    public string Name { get; init; } = name;
    public double Costs { get; set; } = double.MaxValue;
    private List<Edge> Edges { get; } = new ();

    public Node AddEdge(Node node, double cost, bool isAlreadyInOther = false)
    {
        if (Edges.Any(e => e.To == node))
            return this;

        Edges.Add(new Edge(cost, node));
        if (!isAlreadyInOther)
            node.AddEdge(this, cost, true);
        return this;
    }

    public List<Edge> GetEdges()
    {
        return Edges;
    }

    public int CompareTo(Node? other)
    {
        return Costs.CompareTo(other?.Costs ?? double.MaxValue);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }

    public override string ToString()
    {
        string cost = Costs == Double.MaxValue ? "\u221e" : Costs.ToString();
        return $"{Name} <- {cost}";
    }

    public void Reset()
    {
        Costs = int.MaxValue;
    }
}
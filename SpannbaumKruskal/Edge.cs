namespace SpannbaumKruskal;

internal class Edge(Node from, Node to, int cost = 0) : IComparable<Edge>
{
    public Node From { get; set; } = from;
    public Node To { get; set; } = to;
    public int Cost { get; set; } = cost;

    public int CompareTo(Edge? other)
    {
        if (other is null)
            return 1;

        return Cost.CompareTo(other.Cost);
    }

    public bool IsNodeInEdge(Node node) => From.Equals(node) || To.Equals(node);

    public override string ToString()
    {
        return $"[{From.Name} -- {To.Name}]";
    }

    public static Edge Add(Node from, Node to, int cost) => new(from, to, cost);
}

namespace MstHeuristicTsp;

internal class Edge(double cost = 0, Node from = null!, Node to = null!) : IComparable<Edge>
{
    public double Cost { get; } = cost;
    public Node To { get; } = to;
    public Node From { get; } = from;

    public int CompareTo(Edge? other)
    {
        return Cost.CompareTo(other?.Cost ?? double.MaxValue);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(From, To, Cost);
    }

    public override string ToString()
    {
        return "{" + To.Name + "--" + From.Name + "}";
    }
}
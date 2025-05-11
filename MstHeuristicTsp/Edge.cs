namespace MstHeuristicTsp;

internal class Edge(double cost = 0, Node from = null!, Node to = null!)
{
    public double Cost { get; } = cost;
    public Node To { get; } = to;
    public Node From { get; } = from;

    public override int GetHashCode()
    {
        return HashCode.Combine(From, To, Cost);
    }
}
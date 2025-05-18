namespace SpannbaumPrim;

internal class Edge(int cost = 0, Node to = null!)
{
    public int Cost { get; set; } = cost;
    public Node To { get; set; } = to;
}
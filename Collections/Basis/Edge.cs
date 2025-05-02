namespace Collections.Basis;

public class Edge(int cost, BaseNode from, BaseNode to)
{
    public BaseNode To { get; set; } = to;
    public double Cost { get; set; } = cost;
}
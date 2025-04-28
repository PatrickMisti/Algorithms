namespace Collections.Basis;

public class Edge(int? cost, BaseNode? from, BaseNode? to)
{
    public BaseNode? From { get; set; } = from;
    public BaseNode? To { get; set; } = to;
    public int? Cost { get; set; } = cost;
}
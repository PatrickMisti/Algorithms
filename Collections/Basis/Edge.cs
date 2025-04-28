namespace Collections.Basis;

public class Edge
{
    public BaseNode? From { get; set; }
    public BaseNode? To { get; set; }
    public int Cost { get; set; } = 0;
    public Edge(int cost, BaseNode? from, BaseNode? to)
    {
        From = from;
        To = to;
        Cost = cost;
    }
}
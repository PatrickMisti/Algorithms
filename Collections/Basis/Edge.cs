namespace Collections.Basis;

public class Edge(int cost, BaseNode from, BaseNode to) : IComparable<Edge>
{
    public BaseNode To { get; set; } = to;
    public double Cost { get; set; } = cost;


    public int CompareTo(Edge? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (other is null) return 1;
        return Cost.CompareTo(other.Cost);
    }
}
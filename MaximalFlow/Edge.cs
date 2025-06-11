namespace MaximalFlow;

public class Edge
{
    public Node To { get; set; }
    public int Capacity { get; set; }
    public int Flow { get; set; }

    public int ResidualCapacity => Capacity - Flow;
    public Edge Reverse { get; set; }
}
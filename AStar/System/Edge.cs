namespace AStar.System;

public class Edge
{
    public Node? From { get; set; }
    public Node? To { get; set; }
    public int Cost { get; set; } = 0;
    public Edge(int cost, Node? from, Node? to)
    {
        From = from;
        To = to;
        Cost = cost;
    }
}
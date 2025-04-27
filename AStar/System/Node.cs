namespace AStar.System;

public class Node : BaseNode
{
    public int? X { get; set; }
    public int? Y { get; set; }
    public bool IsObstacle { get; set; } = false;

    public Node(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Node(string name, double distance) : base (name, distance)
    {

    }

    public void AddEdge(Node node, int cost)
    {
        if (Edges.Any(e => e.To == node)) return;
        node.Edges.Add(new Edge(cost, from: node, to: this));
        Edges.Add(new Edge(cost, from: this, to: node));
    }

    public bool RemoveEdge(Node node) => node.RemoveEdgeFrom(this) && RemoveEdgeFrom(node);
    
    public bool RemoveEdgeFrom(Node node)
    {
        var item = Edges.Find(opt => opt.From != null && opt.From.Equals(node));
        if (item == null) return false;
        return node.Edges.Remove(item);
    }

    public bool IsValid() => X != null && Y != null || Distance != null;
}
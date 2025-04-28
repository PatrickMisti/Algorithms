using Collections.Basis;

namespace Collections.AStar;

public class Node : BaseNode
{
    public int? X { get; set; }
    public int? Y { get; set; }
    public double? Distance { get; set; }
    public bool IsObstacle { get; set; } = false;

    public Node(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Node(string name, double distance) : base (name)
    {
        Distance = distance;
    }

    public void AddEdges(Node node, int cost) => AddBiEdge(this, node, cost);
    
    public bool RemoveEdges(Node node) => node.RemoveEdge(this) && RemoveEdge(node);

    public bool IsValid() => X != null && Y != null || Distance != null;
}
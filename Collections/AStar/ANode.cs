using Collections.Basis;

namespace Collections.AStar;

public class ANode : BaseNode
{
    public int? X { get; set; }
    public int? Y { get; set; }
    public double? Heuristic { get; set; }
    public double FCost => (Cost + Heuristic ?? 0);
    public bool IsObstacle { get; set; } = false;
    public bool IsVisited { get; set; } = false;

    public ANode(int x, int y)
    {
        X = x;
        Y = y;
    }

    public ANode(string name, double heuristic) : base (name)
    {
        Heuristic = heuristic;
    }

    public void AddEdges(ANode node, int cost) => AddBiEdge(this, node, cost);
    
    public bool RemoveEdges(ANode node) => node.RemoveEdge(this) && RemoveEdge(node);

    public override bool IsValid() => X != null && Y != null || Heuristic != null;
    public override int CompareTo(BaseNode? obj)
    {
        return FCost <= ((ANode)obj!).FCost
            ? -1
            : 1;
    }
}
using Collections.Basis;

namespace Collections.AStar;

public class ANode : BaseNode, IComparable<ANode>
{
    public int? X { get; set; }
    public int? Y { get; set; }
    public double? Heuristic { get; set; }
    public double FCost => (Cost + Heuristic ?? 0);
    public bool IsObstacle { get; set; } = false;

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

    public int CompareTo(ANode? other)
    {
        return FCost <= other?.FCost
            ? -1
            : 1;
    }
}
using Collections.Basis;

namespace Collections.Dijkstra;

public class DNode : BaseNode
{
    public bool IsVisited { get; set; } = false;

    public DNode(string name) : base(name)
    {
    }

    public DNode(DNode node) : base(node.Name)
    {
        Cost = node.Cost;
        Parent = node.Parent;
        IsVisited = node.IsVisited;
    }

    public DNode()
    {
    }

    public override bool IsValid()
    {
        var item = Edges.ToList();
        if (item.Count == 0) return true;

        return true;
    }

    public override int CompareTo(BaseNode? obj)
    {
        return Cost < obj!.Cost ? -1 : 1;
    }
}
using Collections.Basis;

namespace Collections.Dijkstra;

public class DNode : BaseNode
{
    public DNode(string name) : base(name)
    {
    }

    public DNode()
    {
    }

    public override bool IsValid()
    {
        var item = Edges.ToList();
        if (item.Count == 0) return true;

        return item.All(edge => edge is { From: not null, To: not null, Cost: not null });
    }
}
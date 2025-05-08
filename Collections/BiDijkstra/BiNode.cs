
using Collections.Basis;
using Collections.Dijkstra;

namespace Collections.BiDijkstra;

public class BiNode(string name) : DNode(name)
{
    public bool IsVisitedBack { get; set; } = false;
    public double CostBack { get; set; } = double.MaxValue;
    public BaseNode ParentBack { get; set; }

    public void SetVisitedSide(bool isFront)
    {
        if (isFront)
            IsVisited = true;
        else
            IsVisitedBack = true;
    }

    public void SetCost(double cost, bool isFront)
    {
        if (isFront)
            Cost = cost;
        else
            CostBack = cost;
    }

    public double GetCost(bool isFront) => isFront ? Cost : CostBack;

    public double GetTotalCost() => Cost + CostBack;

    public void SetParent(BaseNode parent, bool isFront)
    {
        if (isFront)
            Parent = parent;
        else
            ParentBack = parent;
    }
}
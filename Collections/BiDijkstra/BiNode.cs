
using Collections.Basis;
using Collections.Dijkstra;

namespace Collections.BiDijkstra;

public class BiNode(string name) : DNode(name)
{
    public bool IsVisitedBack { get; set; } = false;
    public double CostBack { get; set; } = double.MaxValue;
}
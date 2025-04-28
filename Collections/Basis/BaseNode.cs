using Collections.AStar;

namespace Collections.Basis;

public class BaseNode
{
    public string Name { get; set; } = string.Empty;
    public double? Distance { get; set; }
    public BaseNode Parent { get; set; } = null!;
    public int Cost { get; set; } = int.MaxValue;
    public bool IsVisited { get; set; }
    public List<Edge> Edges { get; set; } = new();

    public BaseNode()
    {
        
    }

    public BaseNode(string name)
    {
        Name = name;
    }

    public BaseNode(string name, double distance)
    {
        Name = name;
        Distance = distance;
    }
}
using System.Text;

namespace Collections.AStar;

public class Graph
{
    private readonly HashSet<Node> _nodes = new();
    public Node? Start { get; set; }
    public Node? End { get; set; }

    #region Get and Set Nodes functions
    
    public bool AddNodes(Node node, Node other, int cost)
    {
        if (!node.IsValid() || !other.IsValid()) return false;

        node.AddEdges(other, cost);
        _nodes.Add(node);
        _nodes.Add(other);

        return true;
    }

    public void AddNodes(Node node, Node other) => AddNodes(node, other, 1);

    public void RemoveEdge(Node node, Node other) => node.RemoveEdge(other);

    public List<Node> GetNodes() => _nodes.ToList();

    #endregion

    #region Start Condition for AStar

    public void SetVectors(Node start, Node end)
    {
        // set start and end point
        Start = start;
        End = end;
        // if distance is not set, it will calc from coordinates
        CalcDistance();
    }

    private void CalcDistance()
    {
        // calc distance from x and y coordinates
        foreach (var node in _nodes)
            // only if x and y coordinates are not null
            if (node is { X: not null, Y: not null })
                node.Distance = Math.Sqrt(Math.Pow((double)(node.X - End!.X)!, 2) + Math.Pow((double)(node.Y - End!.Y)!, 2));
    }

    #endregion

    #region AStar Algorithm

    public bool FindShortestPath()
    {
        var start = Start;
        var end = End;
        // check if everything is set
        if (start == null || end == null || _nodes.Count < 2)
        {
            Console.WriteLine("Start or end node is not set.");
            return false;
        }

        // calc the shortest path
        Algorithms.AStar.CalcShortestPath(ref start, ref end);
        return true;
    }

    #endregion

    #region Output

    public List<Node> GetShortestPath()
    {
        var path = new List<Node>();
        var current = End;

        while (current != null)
        {
            path.Add(current);
            current = (Node)current.Parent;
        }

        path.Reverse();
        return path;
    }

    public string GetShortestPathToString()
    {
        var path = GetShortestPath();
        var result = new StringBuilder();

        foreach (var node in path)
        {
            result.Append(node.Name);
            if (node != path.Last())
                result.Append(" -> ");
        }

        return result.ToString();
    }

    #endregion

    #region Test

    public void Test()
    {
        var start = _nodes.FirstOrDefault(o => o is { X: 2, Y: 0 });
        var end = _nodes.FirstOrDefault(o => o is { X: 2, Y: 4 });

        var middle = _nodes.FirstOrDefault(o => o is { X: 2, Y: 2 });
        var middle2 = _nodes.FirstOrDefault(o => o is { X: 3, Y: 2 });
        var middle3 = _nodes.FirstOrDefault(o => o is { X: 1, Y: 2 });
        var middle4 = _nodes.FirstOrDefault(o => o is { X: 4, Y: 2 });
        var middle5 = _nodes.FirstOrDefault(o => o is { X: 0, Y: 2 });

        if (start == null || end == null)
        {
            Console.WriteLine("Start or end node not found.");
            return;
        }
        //middle2!.IsObstacle = true;
        middle3!.IsObstacle = true;
        middle4!.IsObstacle = true;
        //middle5!.IsObstacle = true;
        middle!.IsObstacle = true;
        SetVectors(start, end);
    }

    #endregion
}
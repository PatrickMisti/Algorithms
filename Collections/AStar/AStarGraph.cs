using Collections.Algorithms;
using Collections.Basis;

namespace Collections.AStar;

public class AStarGraph : Graph<ANode>
{
    #region Get and Set Nodes functions
    
    public bool AddNodes(ANode node, ANode other, int cost)
    {
        if (!node.IsValid() || !other.IsValid()) return false;

        node.AddEdges(other, cost);
        Nodes.Add(node);
        Nodes.Add(other);

        return true;
    }

    public void AddNodes(ANode node, ANode other) => AddNodes(node, other, 1);

    #endregion

    #region Start Condition for AStar

    public void SetVectors(ANode start, ANode end)
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
        foreach (var node in Nodes)
            // only if x and y coordinates are not null
            if (node is { X: not null, Y: not null })
                node.Distance = Math.Sqrt(Math.Pow((double)(node.X - End!.X)!, 2) + Math.Pow((double)(node.Y - End!.Y)!, 2));
    }

    #endregion

    #region AStar Algorithm

    public override bool PathFinderAlgo()
    {
        var start = Start;
        var end = End;
        // check if everything is set
        if (start == null || end == null || Nodes.Count < 2)
        {
            Console.WriteLine("Start or end node is not set.");
            return false;
        }

        // calc the shortest path
        Algo.CalcShortestPath(ref start, ref end);
        return true;
    }

    #endregion
}
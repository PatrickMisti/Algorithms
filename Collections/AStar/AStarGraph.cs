using Collections.Algorithms;
using Collections.Basis;

namespace Collections.AStar;

public class AStarGraph : Graph<ANode>
{
    #region Get and Set Nodes functions
    
    public bool AddNodes(ANode first, ANode second, int cost)
    {
        if (!AddNodesToList(first, second)) return false;

        first.AddEdges(second, cost);
        return true;
    }

    public void AddNodes(ANode first, ANode second) => AddNodes(first, second, 1);

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
                node.Heuristic = Math.Sqrt(Math.Pow((double)(node.X - End!.X)!, 2) + Math.Pow((double)(node.Y - End!.Y)!, 2));
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
        Algo.AStarAlgo(ref start, ref end);
        return true;
    }

    #endregion
}
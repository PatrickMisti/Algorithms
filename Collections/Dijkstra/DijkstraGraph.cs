using Collections.Algorithms;
using Collections.Basis;

namespace Collections.Dijkstra;

public class DijkstraGraph : Graph<DNode>
{
    public void AddNode(DNode from, DNode to, int cost)
    {
        if (!AddNodesToList(from, to)) return;

        from.AddEdge(to, cost);
    }

    public void SetVectors(DNode start, DNode end)
    {
        // set start and end point
        Start = start;
        End = end;
    }

    public void SetVectors(string first, string last) => SetVectors(GetNodeByName(first), GetNodeByName(last));

    public DNode GetNodeByName(string name)
    {
        var item = Nodes.FirstOrDefault(opt => opt.Name.ToLower().Equals(name.ToLower()));

        if (item == null)
            throw new ArgumentNullException($"Node with name {name} not found");

        return item;
    }

    public override bool PathFinderAlgo()
    {
        var start = Start;
        var end = End;

        if (start == null || end == null || Nodes.Count < 2)
        {
            Console.WriteLine("Start or end node is not set.");
            return false;
        }

        //Algo.DijkstraAlgo(ref start, ref end);
        return true;
    }
}
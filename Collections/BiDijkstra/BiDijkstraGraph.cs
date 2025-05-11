using Collections.Algorithms;
using Collections.Basis;

namespace Collections.BiDijkstra;

public class BiDijkstraGraph : DijGraph<BiNode>
{
    public override bool PathFinderAlgo()
    {
        var start = Start;
        var end = End;

        if (start == null || end == null || Nodes.Count < 2)
        {
            Console.WriteLine("Start or end node is not set.");
            return false;
        }

        Algo.DoubleDijkstraAlgo(ref start, ref end);
        return true;
    }
}
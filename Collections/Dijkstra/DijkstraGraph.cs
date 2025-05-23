﻿using Collections.Algorithms;
using Collections.Basis;

namespace Collections.Dijkstra;

public class DijkstraGraph : DijGraph<DNode>
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

        Algo.DijkstraAlgo(ref start, ref end);
        return true;
    }
}
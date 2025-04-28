using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collections.Basis;

namespace Collections.Dijkstra;

public class DijkstraGraph : Graph<DNode>
{
    public void AddNode(DNode from, DNode to, int cost)
    {
        if (!AddNodesToList(from, to)) return;

        from.AddEdge(to, cost);
    }

    public override bool PathFinderAlgo()
    {
        throw new NotImplementedException();
    }
}
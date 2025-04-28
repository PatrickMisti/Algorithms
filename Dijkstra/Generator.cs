using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Collections.Dijkstra;

namespace Dijkstra;

internal static class Generator
{
    internal static DijkstraGraph CreateGraph()
    {
        var graph = new DijkstraGraph();
        var a = new DNode("A");
        var b = new DNode("B");
        var c = new DNode("C");
        var d = new DNode("D");
        var e = new DNode("E");
        var f = new DNode("F");
        var g = new DNode("G");

        graph.AddNode(a, b, 3);
        graph.AddNode(a, c, 1);

        graph.AddNode(b, d, 3);
        graph.AddNode(b, f, 8);
        graph.AddNode(b, e, 6);

        graph.AddNode(c, b, 5);
        graph.AddNode(c, e, 1);

        graph.AddNode(d, f, 4);

        graph.AddNode(e, g, 17);

        graph.AddNode(f, g, 5);

        graph.SetVectors(a, g);
        return graph;
    }
}
﻿using Collections.BiDijkstra;
using Collections.Dijkstra;

namespace DoubleDijkstra;

internal static class Generator
{
    public static BiDijkstraGraph CreateGraph()
    {
        BiDijkstraGraph graph = new BiDijkstraGraph();
        var s = new BiNode("S");
        var a = new BiNode("A");
        var b = new BiNode("B");
        var c = new BiNode("C");
        var d = new BiNode("D");
        var e = new BiNode("E");
        var f = new BiNode("F");
        var g = new BiNode("G");
        var h = new BiNode("H");
        var i = new BiNode("I");
        var z = new BiNode("Z");

        graph.AddNodes(s, a, 11);
        graph.AddNodes(s, b, 10);
        graph.AddNodes(s, c, 7);
        graph.AddNodes(s, d, 7);
        graph.AddNodes(s, e, 10);
        graph.AddNodes(s, f, 8);

        graph.AddNodes(e, d, 9);
        graph.AddNodes(e, f, 9);

        graph.AddNodes(a, f, 10);
        graph.AddNodes(a, b, 7);

        graph.AddNodes(c, d, 10);
        graph.AddNodes(c, b, 8);

        graph.AddNodes(i, b, 12);
        graph.AddNodes(i, c, 12);
        graph.AddNodes(i, h, 9);

        graph.AddNodes(g, b, 19);
        graph.AddNodes(g, a, 22);
        graph.AddNodes(g, h, 8);

        graph.AddNodes(z, i, 32);
        graph.AddNodes(z, h, 22);
        graph.AddNodes(z, g, 24);


        graph.SetVectors(s, z);

        return graph;
    }
}
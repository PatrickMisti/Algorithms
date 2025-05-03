using Collections.AStar;
using Collections.Dijkstra;

namespace AStarVsDijkstra;

internal static class Generator
{
    public static DijkstraGraph CreateGraphDij()
    {
        var graph = new DijkstraGraph();
        
        var a = new DNode("A");
        var b = new DNode("B");
        var c = new DNode("C");
        var d = new DNode("D");
        var e = new DNode("E");
        var f = new DNode("F");
        var s = new DNode("S");
        var z = new DNode("Z");

        graph.AddNodes(a, b, 3);
        graph.AddNodes(a, c, 3);
        graph.AddNodes(a, z, 4);
        graph.AddNodes(a, d, 2);
        graph.AddNodes(a, f, 4);

        graph.AddNodes(b, c, 10);

        graph.AddNodes(c, z, 4);
        graph.AddNodes(c, d, 1);
        graph.AddNodes(c, e, 2);

        graph.AddNodes(d, z, 2);
        graph.AddNodes(d, s, 7);
        graph.AddNodes(d, f, 9);

        graph.AddNodes(e, s, 3);

        graph.AddNodes(f, s, 2);

        graph.SetVectors(s, z);

        return graph;
    }

    public static AStarGraph CreateGraphAStar()
    {
        var graph = new AStarGraph();

        var a = new ANode("A", 3);
        var b = new ANode("B", 6);
        var c = new ANode("C", 3);
        var d = new ANode("D", 2);
        var e = new ANode("E", 5);
        var f = new ANode("F", 7);
        var s = new ANode("S", 8);
        var z = new ANode("Z", 0);
        // A* graph
        graph.AddNodes(a, b, 3);
        graph.AddNodes(a, c, 3);
        graph.AddNodes(a, z, 4);
        graph.AddNodes(a, d, 2);
        graph.AddNodes(a, f, 4);

        graph.AddNodes(b, c, 10);

        graph.AddNodes(c, z, 4);
        graph.AddNodes(c, d, 1);
        graph.AddNodes(c, e, 2);

        graph.AddNodes(d, z, 2);
        graph.AddNodes(d, s, 7);
        graph.AddNodes(d, f, 9);

        graph.AddNodes(e, s, 3);

        graph.AddNodes(f, s, 2);

        graph.SetVectors(s, z);

        return graph;
    }
}
namespace SpannbaumPrim;

internal class Generator
{
    public static List<Node> GenerateNodes()
    {
        var a = new Node("A");
        var b = new Node("B");
        var c = new Node("C");
        var d = new Node("D");
        var e = new Node("E");
        var f = new Node("F");
        var g = new Node("G");
        var h = new Node("H");
        var i = new Node("I");

        a
            .AddEdge(b, 2)
            .AddEdge(d, 1);

        b
            .AddEdge(a, 2)
            .AddEdge(c, 1)
            .AddEdge(d, 3)
            .AddEdge(e, 2);

        c
            .AddEdge(b, 1)
            .AddEdge(e, 2)
            .AddEdge(f, 7);

        d
            .AddEdge(a, 1)
            .AddEdge(b, 3)
            .AddEdge(e, 2)
            .AddEdge(g, 5);

        e
            .AddEdge(b, 2)
            .AddEdge(c, 2)
            .AddEdge(d, 2)
            .AddEdge(f, 4)
            .AddEdge(g, 5)
            .AddEdge(h, 5);

        f
            .AddEdge(c, 7)
            .AddEdge(e, 4)
            .AddEdge(h, 3)
            .AddEdge(i, 1);

        g
            .AddEdge(d, 5)
            .AddEdge(e, 5)
            .AddEdge(h, 5);

        h
            .AddEdge(e, 5)
            .AddEdge(f, 3)
            .AddEdge(g, 5)
            .AddEdge(i, 3);

        i
            .AddEdge(f, 1)
            .AddEdge(h, 3);

        return [a, b, c, d, e, f, g, h, i];
    }
}
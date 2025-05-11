namespace AllKruskal;

internal class Generator
{
    public static List<Edge> GenerateNodes()
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


        return
        [
            Edge.Add(a, b, 2),
            Edge.Add(a, d, 1),

            Edge.Add(b, c, 1),
            Edge.Add(b, d, 3),
            Edge.Add(b, e, 2),

            Edge.Add(c, e, 2),
            Edge.Add(c, f, 7),

            Edge.Add(d, e, 2),
            Edge.Add(d, g, 5),

            Edge.Add(e, f, 4),
            Edge.Add(e, g, 5),
            Edge.Add(e, h, 5),

            Edge.Add(f, h, 3),
            Edge.Add(f, i, 1),

            Edge.Add(g, h, 5),

            Edge.Add(h, i, 2),
        ];
    }
}
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
            Edge.Add(a, d, 15),

            Edge.Add(g, d, 4),
            Edge.Add(g, h, 4),

            Edge.Add(e, h, 4),
            Edge.Add(e, d, 4),
            Edge.Add(e, b, 4),
            Edge.Add(e, f, 3),

            Edge.Add(c, b, 4),
            Edge.Add(c,f,1),

            Edge.Add(f, i, 3),

        ];
    }
}
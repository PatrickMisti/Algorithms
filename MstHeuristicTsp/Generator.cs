namespace MstHeuristicTsp;

internal class Generator
{
    public static (List<Node>, List<Edge>) CreateList()
    {
        var cityNames = new[] { "Wien", "Linz", "Hagenberg", "Graz", "Salzburg", "Steyr", "Wels" };
        var nodes = cityNames.ToDictionary(name => name, name => new Node(name));

        var input = new List<(string, string, double)>()
        {
            ("Wien", "Linz", 184.4),
            ("Wien", "Hagenberg", 180),
            ("Wien", "Graz", 200.1),
            ("Wien", "Salzburg", 295),
            ("Wien", "Steyr", 166),
            ("Wien", "Wels", 197),
            ("Linz", "Hagenberg", 23),
            ("Linz", "Graz", 220.9),
            ("Linz", "Salzburg", 132.5),
            ("Linz", "Wels", 132.5),
            ("Linz", "Steyr", 132.5),
            ("Salzburg", "Steyr", 134),
            ("Salzburg", "Graz", 296),
            ("Salzburg", "Wels", 108),
            ("Salzburg", "Hagenberg", 155),
            ("Graz", "Steyr", 191),
            ("Graz", "Wels", 196),
            ("Graz", "Hagenberg", 243),
            ("Wels", "Steyr", 45.2),
            ("Wels", "Hagenberg", 57.2),
            ("Steyr", "Hagenberg", 43.6)

        };

        var edges = new List<Edge>();

        foreach (var (from, to, cost) in input)
        {
            edges.Add(new Edge(cost, nodes[from], nodes[to]));
            edges.Add(new Edge(cost, nodes[to], nodes[from]));
        }

        return (nodes.Select(i => i.Value).ToList(), edges);
    }
}
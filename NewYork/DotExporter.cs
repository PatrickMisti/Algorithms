using Collections.Dijkstra;

namespace NewYork;

internal static class DotExporter
{
    public static void ExportToDotWithPath(DijkstraGraph graph, string filePath, List<DNode> path)
    {
        using var writer = new StreamWriter(filePath);
        writer.WriteLine("digraph G {");

        var pathEdges = new HashSet<(string from, string to)>();

        // Schritt 1: Pfad-Kanten extrahieren
        for (int i = 0; i < path.Count - 1; i++)
        {
            pathEdges.Add((path[i].Name, path[i + 1].Name));
        }

        // Schritt 2: Alle Kanten schreiben, Pfad-Kanten farbig
        foreach (var node in graph.GetNodes())
        {
            writer.WriteLine($"    \"{node.Name}\";");

            foreach (var edge in node.Edges)
            {
                string from = node.Name;
                string to = edge.To.Name;
                string label = edge.Cost.ToString();

                bool isPathEdge = pathEdges.Contains((from, to));
                string color = isPathEdge ? "red" : "black";
                string penwidth = isPathEdge ? "3" : "1";

                writer.WriteLine($"    \"{from}\" -> \"{to}\" [label=\"{label}\", color={color}, penwidth={penwidth}];");
            }
        }

        writer.WriteLine("}");
    }
}
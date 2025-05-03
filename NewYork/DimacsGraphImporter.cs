
using Collections.Dijkstra;

namespace NewYork;

internal static class DimacsGraphImporter
{
    public static DijkstraGraph ImportFromGrFile(string filepath)
    {
        var graph = new DijkstraGraph();
        var nodeMap = new Dictionary<int, DNode>();

        foreach (var line in File.ReadLines(filepath))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            var trimmed = line.Trim();
            if (trimmed.StartsWith("c")) continue; // Kommentar
            if (trimmed.StartsWith("p")) continue; // Problemdefinition

            if (trimmed.StartsWith("a"))
            {
                // Beispielzeile: a 1 2 100
                var parts = trimmed.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 4) continue;

                int fromId = int.Parse(parts[1]);
                int toId = int.Parse(parts[2]);
                int cost = int.Parse(parts[3]);

                if (!nodeMap.ContainsKey(fromId))
                    nodeMap[fromId] = new DNode(fromId.ToString());

                if (!nodeMap.ContainsKey(toId))
                    nodeMap[toId] = new DNode(toId.ToString());

                graph.AddNode(nodeMap[fromId], nodeMap[toId], cost);
            }
        }

        return graph;
    }
}
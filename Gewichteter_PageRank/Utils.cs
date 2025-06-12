namespace Gewichteter_PageRank;

internal class Utils
{
    public static List<Edge> ReadFromCSV(string path)
    {
        var list = new List<Edge>();
        foreach (var element in File.ReadLines(path).Skip(1))
        {
            var parts = element.Split(';');
            list.Add(new Edge { From = parts[0].Trim(), To = parts[1].Trim(), Weight = int.Parse(parts[2]) });
        }
        return list;
    }

    public static Dictionary<string, double> PageRank(List<Edge> edges, double d = 1.0, int iterations = 100, double tolerance = 1e-6)
    {
        // Knoten extrahieren
        var nodes = edges.SelectMany(e => new[] { e.From, e.To }).Distinct().ToList();
        var N = nodes.Count;
        // Initialisierung des PageRank-Vektors
        var rank = nodes.ToDictionary(n => n, n => 1.0 / N);

        
        var outgoing = edges
            .GroupBy(e => e.From)
            .ToDictionary(g => g.Key, g => g.ToDictionary(e => e.To, e => (double)e.Weight));

        for (int i = 0; i < iterations; i++)
        {
            var newRank = nodes.ToDictionary(n => n, n => 0.0);

            // PageRank calc
            foreach (var u in nodes)
            {
                if (!outgoing.ContainsKey(u)) continue;

                double sumWeights = outgoing[u].Values.Sum();

                foreach (var v in outgoing[u].Keys)
                {
                    newRank[v] += d * rank[u] * (outgoing[u][v] / sumWeights);
                }
            }

            // Berechnung der Gesamtabwichung zum vorherigen Rank
            double diff = 0;
            foreach (var n in nodes)
            {
                diff += Math.Abs(newRank[n] - rank[n]);
                rank[n] = newRank[n];
            }
            // wenn die Änderung kleiner als die Toleranz ist, abbrechen
            if (diff < tolerance)
                break;
        }

        return rank;
    }

    public static List<Edge> GetInducedSubgraph(List<Edge> edges, HashSet<string> nodesToKeep)
    {
        // Überprüfen, ob die Knoten in der Menge enthalten sind // beide enden im Subgraph enthalten sind
        return edges.Where(e => nodesToKeep.Contains(e.From) && nodesToKeep.Contains(e.To)).ToList();
    }
}
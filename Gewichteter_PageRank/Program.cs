// See https://aka.ms/new-console-template for more information

using Gewichteter_PageRank;

Console.WriteLine("Gewichteter PageRank");

string name = "bsp5.csv";
var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
string file = Path.Combine(path.FullName, name);

var edges = Utils.ReadFromCSV(file);


var ranks = Utils.PageRank(edges, d: 1.0);
var top3 = ranks.OrderByDescending(kvp => kvp.Value).Take(3).Select(kvp => kvp.Key).ToHashSet();

Console.WriteLine("Top 3 Knoten PageRank:");
foreach (var node in top3)
    Console.WriteLine($"- {node} Score: {ranks[node]:F4}");

// Subgraph berechnen
var remainingNodes = edges.SelectMany(e => new[] { e.From, e.To }).Distinct().Where(n => !top3.Contains(n)).ToHashSet();
var subgraph = Utils.GetInducedSubgraph(edges, remainingNodes);

Console.WriteLine("\nSubgraph nach Caesars Angriff:");
foreach (var edge in subgraph)
    Console.WriteLine($"{edge.From} -> {edge.To} Weight: {edge.Weight}");
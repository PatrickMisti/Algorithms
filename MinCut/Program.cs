// See https://aka.ms/new-console-template for more information
using MinCut;

Console.WriteLine("Min Cut");

string name = "bsp4.csv";
var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
string file = Path.Combine(path.FullName, name);

var edges = Utils.ReadFromCSV(file);


var solver = new Calculator(edges);
int maxFlow = solver.MaxFlow("q", "s");
Console.WriteLine($"Maximaler Fluss von q nach s: {maxFlow}");


/*******************************Alice**********************************************/

var cut = solver.GetMinCut("q");
int totalCutWeight = cut.Sum(e => e.Weight);

Console.WriteLine("Minimum Cut Kanten, die Alice entfernen muss: ");
foreach (var edge in cut)
    Console.WriteLine($"Von {edge.From} - Zu {edge.To} Gewicht: {edge.Weight}");

Console.WriteLine("Gesamtrisiko: " + totalCutWeight);

/***********************************Bob******************************************/

var bobEdges = edges.Select(e => new Edge
{
    From = e.From,
    To = e.To,
    Weight = 1
}).ToList();


var bobSolver = new Calculator(bobEdges);
int bobMaxFlow = bobSolver.MaxFlow("q", "s");

var bobCut = bobSolver.GetMinCut("q");

var bobResult = bobCut.Select(cutEdge =>
    edges.First(e =>
        (e.From == cutEdge.From && e.To == cutEdge.To) ||
        (e.From == cutEdge.To && e.To == cutEdge.From)
    )).ToList();

Console.WriteLine("Cut Kanten, die Bob entfernen muss: ");
foreach (var edge in bobResult)
    Console.WriteLine($"Von {edge.From} - Zu {edge.To} Gewicht: {edge.Weight}");

Console.WriteLine($"Gesamtrisiko: {bobResult.Sum(e => e.Weight)}");
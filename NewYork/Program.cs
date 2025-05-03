// See https://aka.ms/new-console-template for more information

using Collections.Dijkstra;
using NewYork;

Console.WriteLine("Welcome JFK Airport");
string startFile = "USA-road-t.NY.gr";
string resultFile = "graph.dot";

string bin = AppContext.BaseDirectory;
string directory = Directory.GetParent(bin)!.Parent!.Parent!.Parent!.FullName;
string filename = Path.Combine(directory, startFile);

var graph1 = DimacsGraphImporter.ImportFromGrFile(filename);
var graph2 = DimacsGraphImporter.ImportFromGrFile(filename);
var graph3 = DimacsGraphImporter.ImportFromGrFile(filename);

// JFK - Newark
graph1.SetVectors("212410","49611");
if (!graph1.PathFinderAlgo())
{
    Console.WriteLine("Path not found");
    return;
}

// JFK - LaGuardia
graph2.SetVectors("212410", "198373");
if (!graph2.PathFinderAlgo())
{
    Console.WriteLine("Path not found");
    return;
}

// Newark - LaGuardia
graph3.SetVectors("49611", "198373");
if (!graph3.PathFinderAlgo())
{
    Console.WriteLine("Path not found");
    return;
}

Console.WriteLine("JFK - Newark");
Console.WriteLine(graph1.GetShortestPathToString());
Console.WriteLine("Newark - LaGuardia");
Console.WriteLine(graph3.GetShortestPathToString());
Console.WriteLine("JFK - LaGuardia");
Console.WriteLine(graph2.GetShortestPathToString());

List<DNode> result = [..graph1.GetShortestPath(), ..graph2.GetShortestPath(), ..graph3.GetShortestPath()];
DotExporter.ExportToDotWithPath(graph1, Path.Combine(directory, resultFile), result);
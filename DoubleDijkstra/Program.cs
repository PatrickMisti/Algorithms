
using Collections.Dijkstra;
using DoubleDijkstra;


Console.WriteLine("Double Dijkstra");

DijkstraGraph graph = Generator.CreateGraph();
if (!graph.PathFinderAlgoDouble())
{
    Console.WriteLine("Path not found");
    return;
}

var result = graph.GetShortestPathToString();
Console.WriteLine($"Path is {result}\n");

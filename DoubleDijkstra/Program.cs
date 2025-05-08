
using Collections.Dijkstra;
using DoubleDijkstra;


Console.WriteLine("Double Dijkstra");

var graph = Generator.CreateGraph();
if (!graph.PathFinderAlgo())
{
    Console.WriteLine("Path not found");
    return;
}

var result = graph.GetShortestPathToString();
Console.WriteLine($"Path is {result}\n");

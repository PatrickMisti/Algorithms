// See https://aka.ms/new-console-template for more information

using Collections.Dijkstra;
using Dijkstra;

Console.WriteLine("Dijkstra Graph");

DijkstraGraph graph = Generator.CreateGraph();

if (!graph.PathFinderAlgo())
{
    Console.WriteLine("No path found");
    return;
}

var result = graph.GetShortestPathToString();
Console.WriteLine($"Path is {result}\n");



// See https://aka.ms/new-console-template for more information

using AStarVsDijkstra;
using Collections.AStar;
using Collections.Dijkstra;

DijkstraGraph dij = Generator.CreateGraphDij();
AStarGraph aStar = Generator.CreateGraphAStar();

Console.WriteLine("Dijkstra");
if (!dij.PathFinderAlgo())
{
    Console.WriteLine("Path not found for Dijkstra");
}

Console.WriteLine("\nA*");
if (!aStar.PathFinderAlgo())
{
    Console.WriteLine("Path not found for A*");
}

Console.WriteLine("--------------------Results----------------------");
Console.WriteLine($"Dijkstra path is {dij.GetShortestPathToString()}");

Console.WriteLine($"A* path is       {aStar.GetShortestPathToString()}");
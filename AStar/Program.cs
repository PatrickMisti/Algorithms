using AStar;
using Collections.AStar;

Console.WriteLine("AStar Algorithm");

Graph graph = Generator.CreateGraphDemo();

if (!graph.FindShortestPath())
{
    Console.WriteLine("No path found");
    return;
}

var result = graph.GetShortestPathToString();
Console.WriteLine($"Path is {result}\n");

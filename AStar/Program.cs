using AStar;
using Collections.AStar;

Console.WriteLine("AStar Algorithm");

AStarGraph graph = Generator.CreateGraphDemo();

if (!graph.PathFinderAlgo())
{
    Console.WriteLine("No path found");
    return;
}

var result = graph.GetShortestPathToString();
Console.WriteLine($"Path is {result}\n");

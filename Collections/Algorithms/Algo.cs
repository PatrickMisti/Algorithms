using System.Collections.Immutable;
using System.Xml.Linq;
using Collections.AStar;
using Collections.Basis;
using Collections.Dijkstra;
using Collections.Extensions;
using static System.Net.Mime.MediaTypeNames;

namespace Collections.Algorithms;

internal static class Algo
{
    public static void AStarAlgo(ref ANode start, ref ANode end)
    {
        HashSet<ANode> queue = new();
        // add start node to queue with cost 0
        start.Cost = 0;
        queue.Add(start);

        while (queue.Count > 0) // shortest path -> && !queue[0].Equals(end)
        {
            // get the node with the lowest cost
            // remove top
            queue.pop_front(out var element);

#if DEBUG
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Current Element = {element.NodeToString()}");
            Console.WriteLine($"Queue: {queue.QueueToStringAStar()}");
            Console.WriteLine($"Child from {element.Name} -> {element.Edges.EdgesToStringAStar()}");
#endif
            // if element is end -> break
            if (element.Equals(end))
                break;

            // if element is already visited, skip it
            if (element.IsVisited) continue;
            element.IsVisited = true;

            Console.WriteLine("Is not visited yet");

            // get all edges from the node
            foreach (var edge in element.Edges)
            {
                // get the other end of edge
                var childNode = (ANode)edge.To;

                // if edge to is obstacle, skip it
                // actually not need for this example
                if (childNode.IsObstacle)
                    continue;

                // calc the current element cost with edge cost
                var newCost = element.Cost + edge.Cost;

                // if the new cost is less than the current cost, set the new cost and parent
                // default childNode cost is int.MaxValue
                if (newCost < childNode.Cost)
                {
                    // set the new cost and parent
                    childNode.Cost = newCost;
                    childNode.Parent = element;
                }

                // add updated element to the queue with the new cost
                queue.Add(childNode);
            }
        }
    }

    public static void DijkstraAlgo(ref DNode start, ref DNode end)
    {
        HashSet<DNode> queue = new();
        start.Cost = 0;

        queue.Add(start);

        while (queue.Count > 0)
        {
            queue.pop_front(out var element);

            if (element == null) break;

            if (element.IsVisited) continue;
            element.IsVisited = true;

#if DEBUG
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Current Element = {element.Name}");
            Console.WriteLine($"Queue: {queue.QueueToStringDij()}");
            Console.WriteLine($"Child from {element.Name} -> {element.Edges.EdgesToStringDij()}");
#endif

            foreach (var edge in element.Edges)
            {
                var childNode = (DNode)edge.To;

                var newCost = element.Cost + edge.Cost;

                if (newCost < childNode.Cost)
                {
                    childNode.Cost = newCost;
                    childNode.Parent = element;
                }

                queue.Add(childNode);
            }
        }
    }

    private static void Print(HashSet<DNode> queue, DNode current, bool front = true)
    {
        string side = front ? "Front" : "Back";

        Console.WriteLine("-----------------------------------------");
        Console.WriteLine($"Current {side}: {current.NodeToString()}");
        Console.WriteLine($"In {side} Queue: {queue.QueueToStringDij()}");
        Console.WriteLine($"Current {side} {current.Name} -> {current.Edges.EdgesToStringDij()}");
    }

    public static bool Step(ref HashSet<DNode> queue, ref HashSet<DNode> otherQueue, ref DNode? current, Func<DNode,DNode,bool>? action = null)
    {
        if (current is null) return false;

        current.IsVisited = true;

        bool fin = false;
        foreach (var edge in current.Edges)
        {
            var childNode = (DNode)edge.To;

            var newCost = current.Cost + edge.Cost;

            if (action is not null)
                fin |= action.Invoke(childNode, current);

            if (newCost < childNode.Cost)
            {
                childNode.Cost = newCost;
                childNode.Parent = current;
            }
            if (!otherQueue.Contains(childNode))
                queue.Add(childNode);
        }
        Print(queue, current, action == null);
        return fin;
    }

    public static void DoubleDijkstraAlgo(ref DNode start, ref DNode end)
    {
        HashSet<DNode> queueFront = new();
        HashSet<DNode> queueBack = new();

        start.Cost = 0;
        queueFront.Add(start);
        
        end.Cost = 0;
        queueBack.Add(end);

        var st = start;
        var en = end;
        bool finished = false;

        while ((queueFront.Count > 0 || queueBack.Count > 0) && !finished)
        {

            queueFront.pop_front_not_visited(out var front);
            queueBack.pop_front_not_visited(out var back);

            finished |= Step(ref queueFront,ref queueBack, ref front);
            finished |= Step(ref queueBack, ref queueFront, ref back,(childNode, element) =>
            {

                if (childNode.SearchToStart(st, en))
                {
                    childNode.CorrectGraph(element);
                    return true;
                }
                return false;
            });
        }

        Console.WriteLine();
    }
}
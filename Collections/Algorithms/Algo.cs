using Collections.AStar;
using Collections.BiDijkstra;
using Collections.Dijkstra;
using Collections.Extensions;

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
            if (element == null) break;

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

                if (!childNode.IsVisited)
                    queue.Add(childNode);
            }
        }
    }

    private static void Print(HashSet<BiNode> queue, DNode current, bool front = true)
    {
        string side = front ? "Front" : "Back";

        Console.WriteLine("-----------------------------------------");
        Console.WriteLine($"Current {side}: {current.NodeToString()}");
        Console.WriteLine($"In {side} Queue: {queue.QueueToStringBiDij(front)}");
        Console.WriteLine($"Current {side} {current.Name} -> {current.Edges.EdgesToStringDij()}");
    }

    public static void Step(ref HashSet<BiNode> queue, ref BiNode? cross, ref double bestPathLength, bool isFront = true)
    {
        queue.pop_front(out var current, isFront);

        if (current is null) return;

        if ((isFront && current.IsVisited) || (!isFront && current.IsVisitedBack))
            return;

        if (isFront)
            current.IsVisited = true;
        else
            current.IsVisitedBack = true;

        foreach (var edge in current.Edges)
        {
            var childNode = (BiNode)edge.To;

            var newCost = current.Cost + edge.Cost;

            if (newCost < childNode.Cost)
            {
                childNode.Cost = newCost;
                childNode.Parent = current;
                queue.Add(childNode);
            }

            bool otherVisited = isFront ? current.IsVisitedBack : current.IsVisited;

            if (!otherVisited) continue;

            double totalCost = newCost + childNode.Cost;
            if (totalCost < bestPathLength)
            {
                bestPathLength = totalCost;
                cross = current;
            }
        }
        Print(queue, current, isFront);
    }

    public static void DoubleDijkstraAlgo(ref BiNode start, ref BiNode end)
    {
        HashSet<BiNode> queueFront = new();
        HashSet<BiNode> queueBack = new();

        start.Cost = 0;
        queueFront.Add(start);
        
        end.Cost = 0;
        queueBack.Add(end);

        BiNode? crossNode = null;
        double bestPathLength = double.MaxValue;

        while (queueFront.Count > 0 || queueBack.Count > 0)
        {
            Step(ref queueFront, ref crossNode, ref bestPathLength);
            /*if (crossNode != null) break;*/
            Step(ref queueBack, ref crossNode, ref bestPathLength, false);
            /*if (crossNode != null) break;*/
        }
        if (crossNode == null) return;
        crossNode.ToEnd();
        Console.WriteLine();
    }
}
using System.Collections.Immutable;
using Collections.AStar;
using Collections.Basis;
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
            var element = queue
                .ToImmutableSortedSet()
                .First();

            // if element is end -> break
            if (element.Equals(end))
                break;

            // remove top
            queue.Remove(element);

#if DEBUG
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Current Element = {element.NodeToString()}");
            Console.WriteLine($"Queue: {queue.QueueToStringAStar()}");
            Console.WriteLine($"Child from {element.Name} -> {element.Edges.EdgesToStringAStar()}");
#endif

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
        PriorityQueue<BaseNode, double> queue = new();
        start.Cost = 0;

        queue.Enqueue(start, start.Cost);
        while (queue.Count > 0)
        {
            queue.TryDequeue(out var element, out var priority);
            if (element!.IsVisited) continue;
            element.IsVisited = true;

            foreach (var edge in element.Edges)
            {
                var childNode = (DNode)edge.To!;
                if (childNode.IsVisited) continue;

                var newCost = childNode.Cost + edge.Cost;
                if (newCost < childNode.Cost)
                {
                    //childNode.Cost = newCost;
                    childNode.Parent = element;
                }
                queue.Enqueue(childNode, childNode.Cost);
            }
        }
    }
}
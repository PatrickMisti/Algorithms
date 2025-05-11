using System.Linq.Expressions;
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

    private static void Print(List<BiNode> queue, DNode current, bool front = true)
    {
        string side = front ? "Front" : "Back";
        queue.Sort((node, biNode) => node.GetCost(front).CompareTo(biNode.GetCost(front)));
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine($"Current {side}: {current.NodeToString()}");
        Console.WriteLine($"In {side} Queue: {string.Join(',', queue.Select(o => o.NodeToString()))}");
        Console.WriteLine($"Current {side} {current.Name} -> {current.Edges.EdgesToStringBiDij()}");
    }

    public static void Step(ref PriorityQueue<BiNode, double> queue, ref HashSet<BiNode> visited, ref BiNode? cross, ref double bestPathLength, bool isFront = true)
    {
        if (!queue.TryDequeue(out var current, out var cost)) return;

        current.SetVisitedSide(isFront);

        if (current is { IsVisitedBack: true, IsVisited: true } && current.GetCost(true) + current.GetCost(false) < bestPathLength)
        {
            cross = current;
            bestPathLength = current.GetTotalCost();
            Console.WriteLine($"Current Cross is {current.Name}");
            return;
        }

        foreach (var edge in current.Edges)
        {
            var childNode = (BiNode)edge.To;
            BiNode? childState = visited.FirstOrDefault(o => o.Equals(childNode));

            var newCost = cost + edge.Cost;

            if (childState == null)
            {
                childNode.SetParent(current, isFront);
                childNode.SetCost(newCost,isFront);
                visited.Add(childNode);
                queue.Enqueue(childNode, newCost);
            }

            else if (newCost < childNode.GetCost(isFront))
            {
                childState.SetCost(newCost, isFront);
                childState.SetParent(current, isFront);
            }
        }
        Print(queue.UnorderedItems.Select(o => o.Element).ToList(), current, isFront);
    }

    public static void DoubleDijkstraAlgo(ref BiNode start, ref BiNode end)
    {
        PriorityQueue<BiNode,double> queueFront = new();
        PriorityQueue<BiNode,double> queueBack = new();
        HashSet<BiNode> visitedFront = new();
        HashSet<BiNode> visitedBack = new();

        start.Cost = 0;
        queueFront.Enqueue(start, 0);
        
        end.CostBack = 0;
        queueBack.Enqueue(end, 0);

        BiNode? crossNode = null;
        double bestPathLength = double.MaxValue;

        while (queueFront.Count > 0 || queueBack.Count > 0)
        {

            Step(ref queueFront, ref visitedFront,ref crossNode, ref bestPathLength);
            Step(ref queueBack, ref visitedBack,ref crossNode, ref bestPathLength, false);

            queueFront.TryPeek(out _, out var minFront);
            queueBack.TryPeek(out _, out var minBack);


            /*if (crossNode != null && bestPathLength <= minFront + minBack)
            {
                break;
            }*/
        }
        if (crossNode == null) return;

        QueueExtensions.Reconstruct(ref crossNode, end);
    }
}
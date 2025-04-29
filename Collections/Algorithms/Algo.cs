using Collections.AStar;
using Collections.Basis;
using Collections.Dijkstra;
using Collections.Extensions;

namespace Collections.Algorithms;

internal static class Algo
{
    public static void AStarAlgo(ref ANode start, ref ANode end)
    {
        List<ANode> queue = new();

        // add start node to queue with cost 0
        start.Cost = 0;
        queue.Add(start);

        while (queue.Count > 0 /*&& !queue[0].Equals(end)*/) // shortest path
        {
            // get the node with the lowest cost
            queue.Sort();
            var element = queue[0];
            queue.RemoveAt(0);

            if (element.Equals(end))
                break;

            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Current Element = {element.Name}:{element.FCost}");
            Console.WriteLine($"Queue: {string.Join(',', queue.Select(o => $"{o.Name}:{o.FCost}"))}");
            Console.WriteLine($"Child from {element.Name} -> {string.Join(',', element.Edges.Select(o => $"{o.To.Name}:{((ANode)o.To).FCost}"))}");
            
            // it's possible to remove edge from element without checking isVisited
            // if element is already visited, skip it
            if (element.IsVisited) continue;

            element.IsVisited = true;

            // get all edges from the node
            foreach (var edge in element!.Edges)
            {
                // get the other end of edge
                var childNode = (ANode)edge.To;
                var cost = edge.Cost;

                // if edge to is obstacle or edge already visited, skip it
                if (childNode.IsObstacle)
                    continue;

                // calc the current element cost with edge cost
                var newCost = element.Cost + cost;

                // if the new cost is less than the current cost, set the new cost and parent
                // default childNode cost is int.MaxValue
                if (childNode.Cost > newCost)
                {
                    // set the new cost and parent
                    childNode.Cost = newCost;
                    childNode.Parent = element;
                    childNode.IsVisited = false;
                }
                // add updated element to the queue with the new cost
                if (!queue.ExistElementInQueue(childNode) && !childNode.IsVisited)
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
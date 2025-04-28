using Collections.AStar;
using Collections.Basis;
using Collections.Dijkstra;

namespace Collections.Algorithms;

internal static class Algo
{
    public static void AStarAlgo(ref ANode start, ref ANode end)
    {
        PriorityQueue<BaseNode, int> queue = new();
        start.Cost = 0;

        // add start node to queue with cost 0
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            // get the node with the lowest cost
            queue.TryDequeue(out var element, out var priority);

            // it's possible to remove edge from element without checking isVisited
            // if element is already visited, skip it
            if (element!.IsVisited) continue;

            // or remove edges
            element.IsVisited = true;

            // get all edges from the node
            foreach (var edge in element.Edges)
            {
                // get the other end of edge
                var childNode = (ANode)edge.To!;

                // if edge is null or edge to is obstacle or edge already visited, skip it
                if (childNode.IsObstacle || childNode.IsVisited)
                    continue;

                // calc the current element cost with edge cost
                var newCost = element.Cost + edge.Cost;

                // if the new cost is less than the current cost, set the new cost and parent
                // default childNode cost is int.MaxValue
                if (newCost < childNode.Cost)
                {
                    // set the new cost and parent
                    childNode.Cost = (int)newCost;
                    childNode.Parent = element;
                }
                // add updated element to the queue with the new cost
                queue.Enqueue(childNode, childNode.Cost);
            }
        }
    }

    public static void DijkstraAlgo(ref DNode start, ref DNode end)
    {

    }
}
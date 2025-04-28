using Collections.AStar;
namespace Collections.Algorithms;

internal static class AStar
{
    public static void CalcShortestPath(ref Node? start, ref Node? end)
    {
        if (start == null || end == null) return;

        PriorityQueue<Node, int> queue = new();
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
                var childNode = edge.To;

                // if edge is null or edge to is obstacle or edge already visited, skip it
                if (childNode == null || childNode.IsObstacle || childNode.IsVisited)
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
                queue.Enqueue(childNode, childNode.Cost);
            }
        }
    }
}
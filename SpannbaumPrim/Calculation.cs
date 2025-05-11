namespace SpannbaumPrim;

internal class Calculation
{
    public static void Calculate(ref List<Node> list, Node? start = null)
    {
        list.ForEach(n => n.Reset());

        var queue = new PriorityQueue<Node, Node>();

        var f = new HashSet<Node>();                 // F
        var current = start ?? list[0];         // v*

        current.Costs = 0;
        queue.Enqueue(current, current);

        while (queue.Count > 0)
        {
            // Get the node with the lowest cost
            current = queue.Dequeue();

            // Skip if already in F
            if (!f.Add(current))
                continue;

            // Go through all edges of the current node without the ones already in F
            foreach (var edge in current.GetEdges(f))
            {
                var to = edge.To;
                var cost = edge.Cost;

                // if the cost is lower than the current cost, update the cost and parent
                if (cost < to.Costs)
                {
                    to.Costs = cost;
                    to.Parent = current;
                    queue.Enqueue(to, to);
                }
            }

            Console.WriteLine($"F    = [ {string.Join(", ", f.Select(o => o.Name))} ]");
            Console.WriteLine($"v*   = {current.Name}");
            Console.WriteLine($"dist = [ {string.Join(", ", list)} ]");
            Console.WriteLine("--------------------------------------------------\n");
        }
    }

    public static void PrintSum(List<Node> list)
    {
        int totalCost = list.Where(n => n.Parent != null).Sum(n => n.Costs);
        Console.WriteLine($"Gesamtkosten des minimalen Spannbaums: {totalCost}\n");
    }

    public static void PrintGraph(List<Node> list)
    {
        Console.WriteLine("graph MST {");
        var result = 
            from n in list
            where n.Parent != null
            select $"  {n.Parent!.Name} -- {n.Name} [label=\"{n.Costs}\"];";

        foreach (var r in result)
            Console.WriteLine(r);

        Console.WriteLine("}");
    }
}
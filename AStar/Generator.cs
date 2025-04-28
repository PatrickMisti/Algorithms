using Collections.AStar;

namespace AStar;

internal static class Generator
{
    internal static Graph CreateGraphDemo()
    {
        Graph graph = new Graph();

        var s = new Node("s", 45);
        var e = new Node("e", 55);
        var d = new Node("d", 48);
        var f = new Node("f", 50);
        var c = new Node("c", 38);
        var a = new Node("a", 40);
        var b = new Node("b", 35);
        var i = new Node("i", 26);
        var h = new Node("h", 20);
        var g = new Node("g", 21);
        var z = new Node("z", 0);

        graph.AddNodes(e, s, 10);
        graph.AddNodes(e, d, 9);
        graph.AddNodes(e, f, 9);
        graph.AddNodes(d, s, 7);
        graph.AddNodes(f, s, 8);
        graph.AddNodes(f, a, 10);
        graph.AddNodes(d, c, 10);
        graph.AddNodes(a, s, 11);
        graph.AddNodes(c, s, 7);
        graph.AddNodes(a, b, 7);
        graph.AddNodes(c, b, 8);
        graph.AddNodes(b, s, 10);
        graph.AddNodes(c, i, 12);
        graph.AddNodes(b, i, 12);
        graph.AddNodes(a, g, 22);
        graph.AddNodes(b, g, 19);
        graph.AddNodes(i, h, 9);
        graph.AddNodes(g, h, 8);
        graph.AddNodes(i, z, 32);
        graph.AddNodes(h, z, 22);
        graph.AddNodes(g, z, 24);

        graph.SetVectors(s, z);

        return graph;
    }

    #region For Grid with x and y
    
    const int N = 5;

    internal static Node[,] GenerateGrid()
    {
        var grid = new Node[N, N];
        for (int x = 0; x < N; x++)
        {
            for (int y = 0; y < N; y++)
            {
                grid[x, y] = new Node(x, y);
            }
        }

        return grid;
    }

    internal static Graph ConvertGridToGraph(Node[,] grid)
    {
        var g = new Graph();

        for (int x = 0; x < N; x++)
        for (int y = 0; y < N; y++)
        {
            if (x > 0) g.AddNodes(grid[x, y], grid[x - 1, y]);
            if (y > 0) g.AddNodes(grid[x, y], grid[x, y - 1]);
            if (x < N - 1) g.AddNodes(grid[x, y], grid[x + 1, y]);
            if (y < N - 1) g.AddNodes(grid[x, y], grid[x, y + 1]);
        }

        return g;
    }

    internal static Graph CreateGraph()
    {
        var grid = GenerateGrid();
        var g = ConvertGridToGraph(grid);
        // Set start and end nodes
        var startNode = grid[2, 0];
        var endNode = grid[2, N - 1];
        g.SetVectors(startNode, endNode);
        // Set obstacles
        /*grid[1, 1].IsObstacle = true;
        grid[2, 2].IsObstacle = true;
        grid[3, 3].IsObstacle = true;*/
        return g;
    }

    internal static void Print(Graph graph)
    {
        var path = new List<Node>();

        var current = graph.End;
        while (current != null)
        {
            path.Add(current);
            current = (Node)current.Parent;
        }

        if (path.Count == 1)
        {
            Console.WriteLine("No path found");
            return;
        }

        var obstacle = graph.GetNodes().Where(opt => opt.IsObstacle);
        path.AddRange(obstacle);
        Console.WriteLine("Path Array");

        for (int x = 0; x < N; x++)
        {
            for (int y = 0; y < N; y++)
            {
                var node = path.FirstOrDefault(opt => opt.X == x && opt.Y == y);

                if (node == null)
                {
                    Console.Write("[ ]");
                    continue;
                }

                if (node.IsObstacle)
                {
                    Console.Write("[O]");
                    continue;
                }

                Console.Write("[X]");
            }

            Console.WriteLine();
        }
    }

    #endregion
}
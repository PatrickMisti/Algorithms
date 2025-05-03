using System.Collections.Immutable;
using Collections.AStar;
using Collections.Basis;
using Collections.Dijkstra;

namespace Collections.Extensions;

internal static class QueueExtensions
{
    public static string EdgesToStringAStar(this IList<Edge> edges) 
        => string.Join(',', edges.Select(o => ((ANode)o.To).NodeToString()));

    public static string QueueToStringAStar(this HashSet<ANode> nodes) 
        => string.Join(',', nodes.Where(o => !o.IsVisited).ToImmutableSortedSet().Select(o => o.NodeToString()));

    public static string NodeToString(this ANode node) 
        => $"{node.Name}:{(node.FCost >= int.MaxValue ? "\u221e" : node.FCost)}";

    public static string QueueToStringDij(this HashSet<DNode> nodes)
        => string.Join(',', nodes.Where(o => !o.IsVisited).Select(o => o.NodeToString()));

    public static string EdgesToStringDij(this IList<Edge> edges) 
        => string.Join(',', edges.Select(o => ((DNode)o.To).NodeToString()));

    public static string NodeToString(this DNode node)
        => $"{node.Name}:{(node.Cost >= int.MaxValue ? "\u221e" : node.Cost)}";

    public static void pop_front(this HashSet<ANode> queue, out ANode? node)
    {
        var element = queue.Where(o => !o.IsVisited).ToImmutableSortedSet()
            .FirstOrDefault();

        if (element == null)
        {
            node = null!;
            return;
        }

        queue.Remove(element);
        node = element;
    }

    public static void pop_front(this HashSet<DNode> queue, out DNode? node)
    {
        var element = queue.Where(o => !o.IsVisited).ToImmutableSortedSet().FirstOrDefault();
        if (element == null)
        {
            node = null;
            return;
        }
        queue.Remove(element);
        node = element;
    }

    public static bool SearchToStart(this DNode node, DNode start, DNode end)
    {
        DNode? tmp = node;
        while (tmp != null && !tmp.Equals(start) && !tmp.Equals(end))
        {
            tmp = (DNode)tmp.Parent;
        }
            

        if (tmp == null) return false;

        if (tmp.Equals(start)) return true;

        return false;
    }

    public static void CorrectGraph(this DNode node, DNode back)
    {
        while (back != null!)
        {
            DNode tmp = (DNode)back.Parent;
            back.Parent = node;
            node = back;
            back = tmp;
        }
    }
}
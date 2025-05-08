using System.Collections.Immutable;
using Collections.AStar;
using Collections.Basis;
using Collections.BiDijkstra;
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

    public static string QueueToStringBiDij(this HashSet<BiNode> nodes, bool isFront)
        => string.Join(',', nodes.Where(o => isFront ? !o.IsVisited : !o.IsVisitedBack).Select(o => o.NodeToString()));

    public static string EdgesToStringDij(this IList<Edge> edges) 
        => string.Join(',', edges.Select(o => ((DNode)o.To).NodeToString()));

    public static string EdgesToStringBiDij(this IList<Edge> edges)
        => string.Join(',', edges.Select(o => ((BiNode)o.To).NodeToString()));

    public static string NodeToString(this DNode node)
        => $"{node.Name}:{(node.Cost >= int.MaxValue ? "\u221e" : node.Cost)}";

    public static string NodeToString(this BiNode node)
        => $"{node.Name}: (F={(node.Cost >= int.MaxValue ? "∞" : node.Cost)}|B={(node.CostBack >= int.MaxValue ? "∞" : node.CostBack)})";

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

    public static void pop_front(this HashSet<BiNode> queue, out BiNode? node, bool isFront)
    {
        var element = queue
            .Where(o => isFront ? !o.IsVisited : !o.IsVisitedBack)
            .ToImmutableSortedSet()
            .FirstOrDefault();

        if (element == null)
        {
            node = null;
            return;
        }
        queue.Remove(element);
        node = element;
    }

    public static void Reconstruct(ref BiNode cross, BiNode end)
    {
        while (!cross.Equals(end))
        {
            cross.ParentBack.Parent = cross;
            cross = (BiNode)cross.ParentBack;
        }
    }

    public static double MinCost(this HashSet<BiNode> queue, bool isFront)
    {
        return queue
            .Where(n => isFront ? !n.IsVisited : !n.IsVisitedBack)
            .Select(n => isFront ? n.Cost : n.CostBack)
            .DefaultIfEmpty(double.MaxValue)
            .Min();
    }
}
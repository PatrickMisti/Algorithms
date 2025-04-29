using System.Collections.Immutable;
using Collections.AStar;
using Collections.Basis;
using Collections.Dijkstra;

namespace Collections.Extensions;

internal static class QueueExtensions
{
    public static bool ExistElementInQueue(this List<ANode> queue, ANode? childNode)
        => queue.Exists(opt => opt.Equals(childNode));
    
    public static string EdgesToStringAStar(this IList<Edge> edges) 
        => string.Join(',', edges.Select(o => ((ANode)o.To).NodeToString()));

    public static string QueueToStringAStar(this HashSet<ANode> nodes) 
        => string.Join(',', nodes.ToImmutableSortedSet().Select(o => o.NodeToString()));

    public static string NodeToString(this ANode node) 
        => $"{node.Name}:{(node.FCost >= int.MaxValue ? "\u221e" : node.FCost)}";

    public static string QueueToStringDij(this HashSet<DNode> nodes)
        => string.Join(',', nodes.Select(o => o.NodeToString()));

    public static string EdgesToStringDij(this IList<Edge> edges) 
        => string.Join(',', edges.Select(o => ((DNode)o.To).NodeToString()));

    public static string NodeToString(this DNode node)
        => $"{node.Name}:{(node.Cost >= int.MaxValue ? "\u221e" : node.Cost)}";

    public static void pop_front(this HashSet<ANode> queue, out ANode node)
    {
        var element = queue.ToImmutableSortedSet()
            .First();

        queue.Remove(element);
        node = element;
    }

    public static void pop_front(this HashSet<DNode> queue, out DNode node)
    {
        var element = queue.ToImmutableSortedSet().First();
        queue.Remove(element);
        node = element;
    }
}
using System.Collections.Immutable;
using Collections.AStar;
using Collections.Basis;

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
}
using Collections.AStar;

namespace Collections.Extensions;

internal static class QueueExtensions
{
    public static bool ExistElementInQueue(this List<ANode> queue, ANode? childNode)
    {
        return queue.Exists(opt => opt.Equals(childNode));
    }
}
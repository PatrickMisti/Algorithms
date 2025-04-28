using System.Text;

namespace Collections.Basis;

public abstract class Graph<T> where T : BaseNode
{
    internal readonly HashSet<T> Nodes = new();
    public T? Start { get; set; }
    public T? End { get; set; }

    #region Get and Set Nodes functions

    public bool AddNodesToList(T? first, T? second, int cost)
    {
        if (first == null || second == null) return false;

        Nodes.Add(first);
        Nodes.Add(second);

        return true;
    }

    public void AddNodesToList(T node, T other) => AddNodesToList(node, other, 1);

    public void RemoveEdge(T node, T other) => node.RemoveEdge(other);

    public List<T> GetNodes() => Nodes.ToList();

    #endregion

    public abstract bool PathFinderAlgo();

    #region Output

    public List<T> GetShortestPath()
    {
        var path = new List<T>();
        var current = End;

        while (current != null)
        {
            path.Add(current);
            current = (T)current.Parent;
        }

        path.Reverse();
        return path;
    }

    public string GetShortestPathToString()
    {
        var path = GetShortestPath();
        var result = new StringBuilder();

        foreach (var node in path)
        {
            result.Append(node.Name);
            if (node != path.Last())
                result.Append(" -> ");
        }

        return result.ToString();
    }

    #endregion
}
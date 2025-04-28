namespace Collections.Basis;

internal abstract class Graph<T> where T : BaseNode
{
    private readonly HashSet<T> _nodes = new();
    public T? Start { get; set; }
    public T? End { get; set; }

    #region Get and Set Nodes functions

    public bool AddNodesToList(T? first, T? second, int cost)
    {
        if (first == null || second == null) return false;

        _nodes.Add(first);
        _nodes.Add(second);

        return true;
    }

    public void AddNodesToList(T node, T other) => AddNodesToList(node, other, 1);

    public void RemoveEdge(T node, T other) => node.RemoveEdge(other);

    public List<T> GetNodes() => _nodes.ToList();

    #endregion

    public abstract bool PathFinderAlgo();
}
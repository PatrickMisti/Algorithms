namespace Collections.Basis;

public abstract class DijGraph <T>: Graph<T> where T : BaseNode
{
    public void AddNode(T from, T to, int cost)
    {
        if (!AddNodesToList(from, to)) return;

        from.AddEdge(to, cost);
    }

    public void AddNodes(T first, T second, int cost)
    {
        if (!AddNodesToList(first, second)) return;
        first.AddBiEdge(first, second, cost);
    }

    public void SetVectors(T start, T end)
    {
        // set start and end point
        Start = start;
        End = end;
    }

    public void SetVectors(string first, string last) => SetVectors(GetNodeByName(first), GetNodeByName(last));

    public T GetNodeByName(string name)
    {
        var item = Nodes.FirstOrDefault(opt => opt.Name.ToLower().Equals(name.ToLower()));

        if (item == null)
            throw new ArgumentNullException($"Node with name {name} not found");

        return item;
    }
}
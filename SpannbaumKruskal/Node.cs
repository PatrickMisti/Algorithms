using System.Xml.Linq;

namespace SpannbaumKruskal;

internal class Node(string name)
{
    public string Name { get; init; } = name;
}
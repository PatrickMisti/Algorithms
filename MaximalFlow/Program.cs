// See https://aka.ms/new-console-template for more information
using MaximalFlow;

Console.WriteLine("Maximal Flow");

var q = new Node { Name = "q" };
var a = new Node { Name = "a" };
var b = new Node { Name = "b" };
var c = new Node { Name = "c" };
var d = new Node { Name = "d" };
var s = new Node { Name = "s" };

Utils.AddEdge(ref q, ref a, 12);
Utils.AddEdge(ref a, ref b, 2);
Utils.AddEdge(ref q, ref b, 4);
Utils.AddEdge(ref a, ref c, 4);
Utils.AddEdge(ref a, ref d, 2);
Utils.AddEdge(ref b, ref d, 3);
Utils.AddEdge(ref c, ref d, 1);
Utils.AddEdge(ref c, ref b, 5);
Utils.AddEdge(ref c, ref s, 3);
Utils.AddEdge(ref d, ref s, 10);

var nodes = new List<Node> { q, a, b, c, d, s };

int maxFlow = Utils.MaxFlow(q, s, nodes);
Console.WriteLine($"Maximaler Fluss: {maxFlow}");


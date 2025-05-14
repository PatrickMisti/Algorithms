// See https://aka.ms/new-console-template for more information

using MstHeuristicTsp;

Console.WriteLine("MST Heuristic TSP");

var (nodes, edges, singleEdges) = Generator.CreateList();

var result = Calculation.Calculate(singleEdges);

Calculation.CombineNodeWithMst(ref nodes, result);

/**************************************************/

var eulerPath = Calculation.CreateEulerKreis(nodes);

if (eulerPath == null)
    Console.WriteLine("No Euler path found.");
else
    Console.WriteLine($"Path EulerKreis:  {string.Join(",", eulerPath.Select(e => e.Name))}");

/******************************************/

var hamilton = Calculation.CreateHamiltonKreis(eulerPath);

Console.WriteLine($"Path Hamilton: {string.Join(",", (hamilton.Select(e => e.Name) ?? null)!)}");


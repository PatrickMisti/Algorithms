// See https://aka.ms/new-console-template for more information
using AllKruskal;

Console.WriteLine("Spannbaum Kruskal\n");

var list = Generator.GenerateNodes();

var result = Calculation.Run(list);

foreach (var r in result)
{
    int cost = 0;
    Console.WriteLine("graph {");
    foreach (var edge in r)
    {
        Console.WriteLine($"  {edge.From.Name} -- {edge.To.Name} [label=\"{edge.Cost}\"];");
        cost += edge.Cost;
    }
    Console.WriteLine("}");
    
    Console.WriteLine($"Total cost: {cost}\n");
}

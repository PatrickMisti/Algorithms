// See https://aka.ms/new-console-template for more information

using SpannbaumPrim;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("Spannbaum Prim\n");

var list = Generator.GenerateNodes();

Calculation.Calculate(ref list);
Calculation.PrintSum(list);
Calculation.PrintGraph(list);

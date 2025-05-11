// See https://aka.ms/new-console-template for more information

using SpannbaumKruskal;

Console.WriteLine("Spannbaum Kruskal\n");

var list = Generator.GenerateNodes();

var result = Calculation.Calculate(list);
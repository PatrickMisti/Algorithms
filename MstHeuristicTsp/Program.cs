// See https://aka.ms/new-console-template for more information

using MstHeuristicTsp;

Console.WriteLine("Hello, World!");

var (nodes, edges) = Generator.CreateList();

var result = Calculation.Calculate(edges);
Calculation.Calculate(edges);
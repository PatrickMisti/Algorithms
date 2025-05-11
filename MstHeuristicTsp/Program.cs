// See https://aka.ms/new-console-template for more information

using MstHeuristicTsp;

Console.WriteLine("Hello, World!");

var (nodes, edges) = Generator.CreateList();

Calculation.Calculate(edges);
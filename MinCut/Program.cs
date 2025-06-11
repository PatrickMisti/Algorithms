// See https://aka.ms/new-console-template for more information
using MinCut;

Console.WriteLine("Min Cut");

string name = "bsp4.csv";
var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
string file = Path.Combine(path.FullName, name);

var edges = Utils.ReadFromCSV(file);




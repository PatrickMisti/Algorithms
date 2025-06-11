using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinCut;

internal class Utils
{
    public static List<Edge> ReadFromCSV(string path)
    {
        var list = new List<Edge>();
        foreach (var element in File.ReadLines(path).Skip(1))
        {
            var parts = element.Split(';');
            list.Add(new Edge { From = parts[0].Trim(), To = parts[1].Trim(), Weight = int.Parse(parts[2]) });
        }
        return list;
    }
}
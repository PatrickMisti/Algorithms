namespace PageRank;

internal class Calculator
{
    public static void CalcWeight(double[,] M, int n)
    {
        double[] R = new double[n];

        for (int i = 0; i < n; i++)
            R[i] = 1.0 / n;

        string[] nodes = new string[n];
        for (int i = 0; i < n; i++)
            nodes[i] = $"Knoten {i + 1}";
        Console.WriteLine($"\t {string.Join(";\t", nodes)}");
        Console.WriteLine($"R0:\t {string.Join(";\t\t", R)}");

        for (int it = 1; it <= 3; it++)
        {
            double[] Rnext = new double[n];
            for (int i = 0; i < n; i++)
            {
                Rnext[i] = 0.0;
                for (int j = 0; j < n; j++)
                    Rnext[i] += M[i, j] * R[j];
            }
            R = Rnext;
            Console.WriteLine($"R{it}: \t{string.Join(";\t\t", R.Select(i => i.ToString("0.###")))}");
        }
    }

    public static double[,] CalcMatrix(double[,] A, double d, int n)
    {
        double[,] M = new double[n, n];
        double tel = (1 - d) / n;

        for (int i = 0; i < n; i++)
            for (int j = 0; j < n; j++)
                M[i, j] = d * A[i, j] + tel;

        return M;
    }

    public static void PrintMatrix(double[,] A, string title = "Matrix")
    {
        Console.WriteLine($"\n{title}:");
        int n = A.GetLength(0);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
                Console.Write($"{A[i, j]:0.###}\t");
            Console.WriteLine();
        }
    }
}
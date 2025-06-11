
using PageRank;

const int         n = 5; // Number of nodes in the graph
const double      d = 0.75; // Damping factor
double[,]         A = new double[n, n];

A[1, 0] = 1.0 / 3; // 1 -> 2
A[2, 0] = 1.0 / 3; // 1 -> 3
A[3, 0] = 1.0 / 3; // 1 -> 4

A[0, 1] = 1.0 / 2; // 2 -> 1
A[2, 1] = 1.0 / 2; // 2 -> 3

A[1, 2] = 1.0;     // 3 -> 2

// 4 is Dangling node, no outgoing edges
for (int i = 0; i < n; i++)
    A[i,3] = 1.0 / n; 

A[0, 4] = 1.0; // 5 -> 1

// M = d * A + (1 - d) / n * E // E is the identity matrix
double[,] M = Calculator.CalcMatrix(A, d, n);
Calculator.PrintMatrix(A, "Adjacency Matrix A");
Calculator.PrintMatrix(M, "Transition Matrix M");
Console.WriteLine();

Calculator.CalcWeight(M, n);


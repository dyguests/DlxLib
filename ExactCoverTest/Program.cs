using System;
using System.Linq;
using DlxLib;

namespace Test
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Test1ColsHasSolutions();
            Test1ColsHasNotSolutions();
            Test2Cols();
            TestSample();
        }

        private static void Test1ColsHasSolutions()
        {
            var matrix = new[,]
            {
                {1},
            };
            Print(matrix);
        }

        private static void Test1ColsHasNotSolutions()
        {
            var matrix = new[,]
            {
                {0},
            };
            Print(matrix);
        }

        private static void Test2Cols()
        {
            var matrix = new[,]
            {
                {1, 0},
                {0, 1},
                {1, 1},
                {0, 0},
            };
            Print(matrix);
        }

        private static void TestSample()
        {
            var matrix = new[,]
            {
                {1, 0, 0, 1, 0, 0, 1},
                {1, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 1, 1, 0, 1},
                {0, 0, 1, 0, 1, 1, 0},
                {0, 1, 1, 0, 0, 1, 1},
                {0, 1, 0, 0, 0, 0, 1},
            };
            Print(matrix);
        }

        private static void Print(int[,] matrix)
        {
            Console.WriteLine("-------- begin --------");
            Console.WriteLine(matrix.MatrixToString());
            Dlx.Solve(matrix);
            Console.WriteLine("-------- end --------");
        }

        private static string MatrixToString(this int[,] matrix)
        {
            return string.Join("\n", matrix.OfType<int>()
                .Select((value, index) => new {value, index})
                .GroupBy(x => x.index / matrix.GetLength(1))
                .Select(x => $"{{{string.Join(",", x.Select(y => y.value))}}}"));
        }
    }
}
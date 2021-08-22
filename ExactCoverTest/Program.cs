using System;
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

        private static void Print(int[,] matrix)
        {
            Console.WriteLine("-------- begin --------");
            Dlx.Solve(matrix);
            Console.WriteLine("-------- end --------");
        }
    }
}
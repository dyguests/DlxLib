using System;
using DlxLib;

namespace Test
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var matrix = new[,]
            {
                // {1, 0},
                // {0, 1},
                {1, 1},
                // {0, 0},
            };
            Console.WriteLine(Dlx.Solve(matrix));
        }
    }
}
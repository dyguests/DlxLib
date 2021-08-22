using System;
using System.Collections.Generic;
using System.Linq;
using DlxLib;
using NUnit.Framework;

namespace UniqueTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1Solutions()
        {
            var matrix = new[,]
            {
                {1, 0},
                {0, 1},
                {0, 0},
            };
            var solutions = Validate(matrix);
            Assert.AreEqual(1, solutions.Count());
        }

        [Test]
        public void Test2Solutions()
        {
            var matrix = new[,]
            {
                {1, 0},
                {0, 1},
                {1, 1},
                {0, 0},
            };
            var solutions = Validate(matrix);
            Assert.AreNotEqual(1, solutions.Count());
        }

        [Test]
        public void TestMoreThan2Solutions()
        {
            var matrix = new[,]
            {
                {1, 0},
                {0, 1},
                {1, 1},
                {1, 1},
            };
            var solutions = Validate(matrix);
            Assert.AreEqual(2, solutions.Count());
        }

        private static IEnumerable<int[]> Validate(int[,] matrix)
        {
            Console.WriteLine("-------- begin --------");
            Console.WriteLine(matrix.MatrixToString());
            var solutions = Dlx.Solve(matrix).ToArray();
            foreach (var solution in solutions)
            {
                Console.WriteLine("Solution:" + String.Join(",", solution));
            }

            Console.WriteLine("Unique:" + (solutions.Length == 1));
            Console.WriteLine("-------- end --------");
            return solutions;
        }
    }
}
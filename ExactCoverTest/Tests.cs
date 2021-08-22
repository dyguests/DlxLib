using System;
using DlxLib;
using NUnit.Framework;
using TestUtil;

namespace ExactCoverTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestEmpty()
        {
            var matrix = new int[0, 0];
            Validate(matrix);
            Assert.True(true);
        }

        [Test]
        public void Test1ColsHasSolutions()
        {
            var matrix = new[,]
            {
                {1},
            };
            Validate(matrix);
            Assert.True(true);
        }

        [Test]
        public void Test1ColsHasNotSolutions()
        {
            var matrix = new[,]
            {
                {0},
            };
            Validate(matrix);
            Assert.True(true);
        }

        [Test]
        public void Test2Cols()
        {
            var matrix = new[,]
            {
                {1, 0},
                {0, 1},
                {1, 1},
                {0, 0},
            };
            Validate(matrix);
            Assert.True(true);
        }

        [Test]
        public void TestSample()
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
            Validate(matrix);
            Assert.True(true);
        }

        [Test]
        public void TestSecondary()
        {
            var matrix = new[,]
            {
                {1, 0, 1, 0},
                {0, 1, 1, 0},
                {0, 1, 0, 1},
                {0, 1, 0, 0},
            };
            Validate(matrix, 2);
            Assert.True(true);
        }

        private static void Validate(int[,] matrix, int numPrimaryColumns = int.MaxValue)
        {
            Console.WriteLine("-------- begin --------");
            Console.WriteLine(matrix.MatrixToString());
            foreach (var result in Dlx.Solve(matrix, numPrimaryColumns, new Dlx.Instrumentation[0]))
            {
                Console.WriteLine("Solution:" + String.Join(",", result));
            }

            Console.WriteLine("-------- end --------");
        }
    }
}
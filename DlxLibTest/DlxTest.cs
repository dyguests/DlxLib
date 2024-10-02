using System;
using System.Collections.Generic;
using DlxLib;
using ExactCoverTest;
using NUnit.Framework;

namespace DlxLibTest
{
    class DlxTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void Test1()
        {
            // 确切覆盖问题矩阵
            var matrix = new[,]
            {
                { 1, 0, 0, 1, 0, 0, 1 },
                { 1, 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 1, 0, 1 },
                { 0, 0, 1, 0, 1, 1, 0 },
                { 0, 1, 1, 0, 0, 1, 1 },
                { 0, 1, 0, 0, 0, 0, 1 }
            };

            var columnNames = new List<string> { "A", "B", "C", "D", "E", "F", "G" };

            var dlx = new Dlx(matrix, columnNames);
            dlx.Search();
            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            // 确切覆盖问题矩阵
            var matrix = new[,]
            {
                { 1, 0, 0, 1, 0, 0, 1 },
                { 1, 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 1, 0, 1 },
                { 0, 0, 1, 0, 1, 1, 0 },
                { 0, 1, 1, 0, 0, 1, 1 },
                { 0, 1, 0, 0, 0, 0, 1 }
            };

            var dlx = new Dlx(matrix);
            dlx.Search();
            Assert.Pass();
        }

        private static void Validate(int[,] matrix, int numPrimaryColumns = int.MaxValue)
        {
            Console.WriteLine("-------- begin --------");
            Console.WriteLine("matrix:\n" + matrix.MatrixToString());
            if (numPrimaryColumns != int.MaxValue)
            {
                Console.WriteLine("numPrimaryColumns:" + numPrimaryColumns);
            }

            Console.WriteLine("Solutions:");

            var dlx = new Dlx(matrix);
            // foreach (var result in Dlx.Search())
            // {
            //     Console.WriteLine("Solution:" + String.Join(",", result));
            // }

            Console.WriteLine("-------- end --------");

            Assert.Pass();
        }
    }
}
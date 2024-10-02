using System.Collections.Generic;
using DlxLib;
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

            var solver = new Dlx(matrix, columnNames);
            solver.Search();
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

            var solver = new Dlx(matrix);
            solver.Search();
            Assert.Pass();
        }
    }
}
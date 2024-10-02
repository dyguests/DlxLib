using NUnit.Framework;

namespace DlxLib
{
    class Program
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void Test1()
        {
            // 确切覆盖问题矩阵
            int[,] matrix = new int[,]
            {
                { 1, 0, 0, 1, 0, 0, 1 },
                { 1, 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 1, 0, 1 },
                { 0, 0, 1, 0, 1, 1, 0 },
                { 0, 1, 1, 0, 0, 1, 1 },
                { 0, 1, 0, 0, 0, 0, 1 }
            };

            List<string> columnNames = new List<string> { "A", "B", "C", "D", "E", "F", "G" };

            DLXSolver solver = new DLXSolver(matrix, columnNames);
            solver.Search(0);
            Assert.Pass();
        }
    }
}
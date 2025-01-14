using System;
using System.Linq;
using DlxLib;
using ExactCoverTest;
using NUnit.Framework;

namespace DlxLibTest
{
    public class DlxTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestEmpty()
        {
            var matrix = new int[0, 0];
            Validate(matrix);
            Assert.Pass();
        }

        [Test]
        public void Test1ColsHasSolutions()
        {
            var matrix = new[,]
            {
                { 1 },
            };
            Validate(matrix);
            Assert.Pass();
        }

        [Test]
        public void Test1ColsHasNotSolutions()
        {
            var matrix = new[,]
            {
                { 0 },
            };
            Validate(matrix);
            Assert.Pass();
        }

        [Test]
        public void Test2Cols()
        {
            var matrix = new[,]
            {
                { 1, 0 },
                { 0, 1 },
                { 1, 1 },
                { 0, 0 },
            };
            Validate(matrix);
            Assert.Pass();
        }

        [Test]
        public void TestSample()
        {
            var matrix = new[,]
            {
                { 1, 0, 0, 1, 0, 0, 1 },
                { 1, 0, 0, 1, 0, 0, 0 },
                { 0, 0, 0, 1, 1, 0, 1 },
                { 0, 0, 1, 0, 1, 1, 0 },
                { 0, 1, 1, 0, 0, 1, 1 },
                { 0, 1, 0, 0, 0, 0, 1 },
            };
            Validate(matrix);
            Assert.Pass();
        }

        [Test]
        public void TestSecondary()
        {
            var matrix = new[,]
            {
                { 1, 0, 1, 0 },
                { 0, 1, 1, 0 },
                { 0, 1, 0, 1 },
                { 0, 1, 0, 0 },
            };
            Validate(matrix, new[] { 0, 0, 1, 1 });
            Assert.Pass();
        }

        [Test]
        public void TestColumns()
        {
            var matrix = new[,]
            {
                { 1, 0, 1, 0 },
                { 0, 1, 1, 0 },
                { 0, 1, 0, 1 },
                { 0, 1, 0, 0 },
            };
            Validate(matrix);
            Assert.Pass();
        }

        [Test]
        public void TestColumns2()
        {
            var matrix = new[,]
            {
                { 1, 0, 1, 1, 0 },
                { 0, 1, 1, 0, 0 },
                { 0, 1, 0, 1, 1 },
                { 0, 1, 0, 0, 0 },
            };
            Validate(matrix);
            Assert.Pass();
        }

        [Test]
        public void TestColumns3()
        {
            var matrix = new[,]
            {
                { 1, 0, 1, 0 },
                { 0, 1, 1, 0 },
                { 0, 1, 0, 0 },
                { 0, 1, 0, 0 },
            };
            Validate(matrix);
            Assert.Pass();
        }

        [Test]
        public void TestHintColumns()
        {
            var matrix = new[,]
            {
                //P  P     S  P  S
                { 1, 0, 1, 1, 1, 0 },
                { 0, 1, 1, 1, 0, 0 },
                { 0, 1, 1, 0, 1, 1 },
                { 0, 1, 1, 0, 0, 0 },
            };
            Validate(matrix);
            Assert.Pass();
        }

        [Test]
        public void TestColumnNames()
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

            var columnNames = new[] { "A", "B", "C", "D", "E", "F", "G" };

            var dlx = new Dlx(matrix /*, columnNames: columnNames*/);
            dlx.Solve();
            Assert.Pass();
        }

        [Test]
        public void TestPrint()
        {
            // 确切覆盖问题矩阵
            var matrix = new[,]
            {
                { 1, 0, 1, 0, },
                { 0, 1, 0, 0, },
                { 0, 1, 1, 0, },
            };

            Validate(matrix, new[] { 1, 1, 0, 0, });
        }

        [Test]
        public void TestIgnoreColumn()
        {
            // 确切覆盖问题矩阵
            var matrix = new[,]
            {
                { 1, 0, 1, 0, },
                { 0, 1, 0, 0, },
                { 0, 1, 1, 0, },
            };

            Validate(matrix, new[] { 0, 0, 9, 1, });
        }

        [Test]
        public void TestIgnoreColumn2()
        {
            // 确切覆盖问题矩阵
            var matrix = new[,]
            {
                { 1, 0, 1, 1, },
                { 0, 1, 0, 1, },
                { 0, 1, 1, 0, },
            };

            Validate(matrix, new[] { 0, 0, 9, 0, });
        }

        [Test]
        public void TestIgnoreColumn3()
        {
            // 确切覆盖问题矩阵
            var matrix = new[,]
            {
                { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, },
                { 0, 1, 2, 0, 0, 0, 0, 0, 0, 0, 0, 1, },
                { 0, 1, 2, 0, 0, 0, 0, 0, 0, 0, 1, 0, },
            };

            Validate(matrix, new[] { 0, 0, 9, 0, 0, 0, 0, 0, 0, 0, 0, 0, });
        }

        [Test]
        public void TestSecondaryColumn()
        {
            // 确切覆盖问题矩阵
            var columnPredicate = new[]
                { 0, 0, 0, 1, 1, 1, };
            var matrix = new[,]
            {
                { 1, 0, 0, 1, 0, 0, },
                { 1, 0, 0, 0, 1, 0, },
                { 1, 0, 0, 0, 0, 1, },
                { 0, 1, 0, 1, 0, 0, },
                { 0, 1, 0, 0, 1, 0, },
                { 0, 1, 0, 0, 0, 1, },
                { 0, 0, 1, 1, 0, 0, },
                { 0, 0, 1, 0, 1, 0, },
                { 0, 0, 1, 0, 0, 1, },
            };

            Validate(matrix, columnPredicate);
        }

        [Test]
        public void TestSecondaryColumn2()
        {
            // 确切覆盖问题矩阵
            var columnPredicate = new[]
                { 0, 0, 1, 1, 1, };
            var matrix = new[,]
            {
                { 1, 0, 1, 0, 0, },
                { 1, 0, 0, 1, 0, },
                { 1, 0, 0, 0, 1, },
                { 0, 1, 1, 0, 0, },
                { 0, 1, 0, 1, 0, },
                { 0, 1, 0, 0, 1, },
                // { 0, 0, 1, 1, 0, 0, },
                // { 0, 0, 1, 0, 1, 0, },
                // { 0, 0, 1, 0, 0, 1, },
            };

            Validate(matrix, columnPredicate);
        }

        [Test]
        public void TestSecondaryColumn3()
        {
            // 确切覆盖问题矩阵
            var columnPredicate = new[]
                { 0, 0, 0, 1, 1, 1, };
            var matrix = new[,]
            {
                { 1, 1, 0, 1, 0, 0, },
                { 1, 1, 0, 0, 1, 0, },
                { 1, 1, 0, 0, 0, 1, },
                { 0, 1, 1, 1, 0, 0, },
                { 0, 1, 1, 0, 1, 0, },
                { 0, 1, 1, 0, 0, 1, },
            };

            Validate(matrix, columnPredicate);
        }

        [Test]
        public void TestSecondaryColumn4()
        {
            // 确切覆盖问题矩阵
            var columnPredicate = new[]
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, };
            var matrix = new[,]
            {
                { 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, },
                { 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, },
                { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, },
                { 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, },
                { 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, },
                { 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, },
                { 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, },
                { 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, },
                { 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, },
                { 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, },
                { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, },
                { 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, },
                { 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, },
                { 0, 0, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, },
                { 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, },
                { 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, },
            };

            Validate(matrix, columnPredicate);
        }

        private static void Validate(int[,] matrix, int[]? columnPredicate = null)
        {
            Console.WriteLine("-------- begin --------");
            Console.WriteLine("matrix:\n" + matrix.MatrixToString());
            if (columnPredicate != null)
            {
                Console.WriteLine("columnPredicate:\n" + string.Join(", ", columnPredicate));
            }

            Console.WriteLine("Solutions:");

            var dlx = columnPredicate == null ? new Dlx(matrix) : new Dlx(matrix, columnPredicate);

            foreach (var solution in dlx.Solve())
            {
                var rowIndexes = solution.RowIndexes.OrderBy(i => i);
                Console.WriteLine($"Solution:{string.Join(",", rowIndexes)} deep:{solution.Deep}");
            }

            Console.WriteLine("-------- end --------");

            Assert.Pass();
        }
    }
}
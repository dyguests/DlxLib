using System;
using NUnit.Framework;
using SudokuDlxLib;
using SudokuDlxLib.Utils;

namespace SudokuTest
{
    [TestFixture]
    public class ArrayTests
    {
        [Test]
        public void TestArray()
        {
            var array = new int[0];
            Console.WriteLine("array:" + string.Join(",", array));
            array = array.Add(1);
            Console.WriteLine("array:" + string.Join(",", array));
            Assert.True(true);
        }

        [Test]
        public void TestToArrayIEnumerable()
        {
            var matrix = new int[2, 3]
            {
                {1, 2, 3},
                {4, 5, 6},
            };
            foreach (var array in ArrayUtil.ToArrayIEnumerable(matrix))
            {
                Console.WriteLine("array:" + string.Join(",", array));
            }

            Assert.True(true);
        }

        [Test]
        public void TestExpandMatrix()
        {
            var curr = new Matrix
            {
                matrix = new int[,]
                {
                    {1, 0, 1},
                    {0, 1, 0},
                },
            };
            Console.WriteLine("matrix curr:\n" + curr.matrix.MatrixToString());
            var other = new Matrix
            {
                matrix = new int[,]
                {
                    {1, 0, 0, 1},
                    {1, 0, 1, 0},
                    {0, 1, 0, 0},
                },
            };
            Console.WriteLine("matrix other:\n" + other.matrix.MatrixToString());

            curr.Expand(other, new int[0]);

            Console.WriteLine("matrix result:\n" + curr.matrix.MatrixToString());

            Assert.True(true);
        }

        [Test]
        public void TestExpandMatrix2()
        {
            var curr = new Matrix
            {
                matrix = new int[,]
                {
                    {1, 0, 1},
                    {0, 1, 0},
                },
                primaryColumns = new int[] {2},
            };
            MatrixUtil.PrintMatrix(curr);
            var other = new Matrix
            {
                matrix = new int[,]
                {
                    {1, 0, 0, 1},
                    {1, 0, 1, 0},
                    {0, 1, 0, 0},
                },
                secondaryColumns = new int[] {2},
            };
            MatrixUtil.PrintMatrix(other);

            curr.Expand(other, new[] {0});

            MatrixUtil.PrintMatrix(curr);

            Assert.True(true);
        }

        [Test]
        public void TestExpandMatrix3()
        {
            var curr = new Matrix
            {
                matrix = new int[,]
                {
                    {1, 0, 1},
                    {0, 1, 0},
                },
                primaryColumns = new int[] {2},
                secondaryColumns = new int[0],
            };
            MatrixUtil.PrintMatrix(curr);

            var other = new Matrix
            {
                matrix = new int[,]
                {
                    {1, 0, 0, 1},
                    {1, 0, 1, 0},
                    {0, 1, 0, 1},
                    {0, 0, 1, 1},
                },
                primaryColumns = new int[] {3},
                secondaryColumns = new int[] {2},
            };
            MatrixUtil.PrintMatrix(other);

            curr.Expand(other, new[] {0, 1});
            MatrixUtil.PrintMatrix(curr);

            Assert.True(true);
        }
    }
}
using System;
using System.Linq;
using DlxLib;
using Entities;
using NUnit.Framework;
using SudokuTest.Datas;
using SudokuTest.Utils;

namespace SudokuTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestSimpleBaseSudoku()
        {
            var puzzle = PuzzleDatas.BaseDatas[0];
            var solutions = Validate(puzzle);
            Assert.AreEqual(1, solutions.Length);
        }

        [Test]
        public void TestExtremelyBaseSudoku()
        {
            var puzzle = PuzzleDatas.BaseDatas[1];
            var solutions = Validate(puzzle);
            Assert.AreEqual(1, solutions.Length);
        }

        [Test]
        public void TestKillerSudoku()
        {
            var puzzle = PuzzleDatas.KillerDatas[0];
            var solutions = Validate(puzzle);
            Assert.AreEqual(1, solutions.Length);
        }

        private static int[][] Validate(Puzzle puzzle)
        {
            Console.WriteLine("-------- begin --------");
            Console.WriteLine("Puzzle:\n" + puzzle.PuzzleToString());
            var matrix = DlxUtil.ToMatrix(puzzle);
            // Console.WriteLine(matrix.MatrixToString());
            var solutions = Dlx.Solve(matrix).ToArray();
            foreach (var solution in solutions)
            {
                Console.WriteLine("Solution:" + String.Join(",", solution));
            }

            foreach (var solution in solutions)
            {
                var answer = DlxUtil.ToNumbers(matrix, solution);
                Console.WriteLine("Answer:\n" + string.Join(
                                      "\n",
                                      answer
                                          .Select((value, index) => new {value, index})
                                          .GroupBy(x => x.index / 9)
                                          .Select(x => string.Join("", x.Select(y => y.value))))
                );
            }

            Console.WriteLine("-------- end --------");
            return solutions;
        }
    }
}
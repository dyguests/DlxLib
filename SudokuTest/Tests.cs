using System;
using System.Linq;
using DlxLib;
using NUnit.Framework;
using SudokuDlxLib;
using SudokuDlxLib.Utils;
using SudokuLib;

namespace SudokuTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestNormalSudoku()
        {
            var sudoku = new Sudoku
            {
                initNumbers = new[] {0, 0, 0, 0, 0, 8, 3, 0, 0, 0, 6, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 8, 5, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1, 0, 2, 4, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 5, 0, 0},
                rules = new Rule[]
                {
                    new NormalRule(),
                }
            };
            Console.WriteLine("Sudoku:\n" + sudoku.initNumbers.NumbersToString());

            var (matrix, primaryColumns, secondaryColumns) = SudokuDlxUtil.SudokuToMatrix(sudoku);
            var solutions = Dlx.Solve(matrix, primaryColumns, secondaryColumns).ToArray();
            foreach (var result in solutions)
            {
                Console.WriteLine("Solution:" + String.Join(",", result));
            }

            Assert.True(solutions.Length == 1);

            if (solutions.Length == 1)
            {
                var solutionNumbers = SudokuDlxUtil.SolutionToNumbers(sudoku, matrix, solutions[0]);

                Console.WriteLine("Sudoku Solution:\n" + solutionNumbers.NumbersToString());
            }
        }

        [Test]
        public void TestKillerSudoku()
        {
            var sudoku = new Sudoku
            {
                initNumbers = new[] {0, 0, 0, 0, 0, 8, 3, 0, 0, 0, 6, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 8, 5, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1, 0, 2, 4, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 5, 0, 0},
                rules = new Rule[]
                {
                    new NormalRule(),
                    new CageRule
                    {
                        cages = new[]
                        {
                            new CageRule.Cage {sum = 12, indexes = new[] {0, 1}},
                        },
                    },
                }
            };
            Console.WriteLine("Sudoku:\n" + sudoku.initNumbers.NumbersToString());

            var (matrix, primaryColumns, secondaryColumns) = SudokuDlxUtil.SudokuToMatrix(sudoku);
            var solutions = Dlx.Solve(matrix, primaryColumns, secondaryColumns).ToArray();
            Console.WriteLine("Solution:\n");
            foreach (var result in solutions)
            {
                Console.WriteLine(String.Join(",", result));
            }

            Assert.True(solutions.Length == 1);

            if (solutions.Length == 1)
            {
                var solutionNumbers = SudokuDlxUtil.SolutionToNumbers(sudoku, matrix, solutions[0]);

                Console.WriteLine("Sudoku Solution:\n" + solutionNumbers.NumbersToString());
            }
        }

        [Test]
        public void TestArray()
        {
            var array = new int[0];
            Console.WriteLine("array:" + string.Join(",", array));
            array = array.Add(1);
            Console.WriteLine("array:" + string.Join(",", array));
            Assert.True(true);
        }
    }
}
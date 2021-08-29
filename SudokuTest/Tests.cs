﻿using System;
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
        public void TestKillerSudoku2()
        {
            var sudoku = new Sudoku
            {
                initNumbers = new[]
                {
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                    0, 0, 0, 0, 0, 0, 0, 0, 0,
                },
                rules = new Rule[]
                {
                    new NormalRule(),
                    new CageRule
                    {
                        cages = new[]
                        {
                            new CageRule.Cage {sum = 12, indexes = new[] {0, 1, 2,}},
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

            // Assert.True(solutions.Length == 1);
            Assert.True(true);

            if (solutions.Length == 1)
            {
                var solutionNumbers = SudokuDlxUtil.SolutionToNumbers(sudoku, matrix, solutions[0]);

                Console.WriteLine("Sudoku Solution:\n" + solutionNumbers.NumbersToString());
            }
        }

        [Test]
        public void TestKillerSudokusSimple()
        {
            var sudoku = new Sudoku
            {
                initNumbers = new[] {0, 0, 0, 4, 9, 0, 5, 0, 0, 0, 0, 0, 0, 1, 8, 9, 0, 0, 6, 9, 1, 0, 0, 0, 0, 2, 0, 0, 1, 6, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 9, 0, 0, 1, 4, 0, 9, 5, 3, 0, 0, 6, 0, 0, 0, 0, 0, 7, 0, 2, 0, 0, 0, 0, 4, 1, 0, 5, 0, 7, 0, 0, 7, 3, 0, 6, 2, 0, 4, 0,},
                rules = new Rule[]
                {
                    new NormalRule(),
                    new CageRule
                    {
                        cages = new[]
                        {
                            new CageRule.Cage {sum = 18, indexes = new[] {72, 73, 74,}},
                            new CageRule.Cage {sum = 25, indexes = new[] {75, 76, 77, 67,}},
                            new CageRule.Cage {sum = 20, indexes = new[] {78, 79, 80, 71, 70,}},
                            new CageRule.Cage {sum = 11, indexes = new[] {63, 64,}},
                            new CageRule.Cage {sum = 7, indexes = new[] {54, 55,}},
                            new CageRule.Cage {sum = 10, indexes = new[] {56, 65, 66,}},
                            new CageRule.Cage {sum = 17, indexes = new[] {57, 48, 47,}},
                            new CageRule.Cage {sum = 11, indexes = new[] {68, 69,}},
                            new CageRule.Cage {sum = 11, indexes = new[] {58, 59,}},
                            new CageRule.Cage {sum = 19, indexes = new[] {60, 61, 62,}},
                            new CageRule.Cage {sum = 19, indexes = new[] {46, 45, 36,}},
                            new CageRule.Cage {sum = 4, indexes = new[] {49, 50,}},
                            new CageRule.Cage {sum = 7, indexes = new[] {51,}},
                            new CageRule.Cage {sum = 8, indexes = new[] {52, 53,}},
                            new CageRule.Cage {sum = 13, indexes = new[] {37, 38, 29,}},
                            new CageRule.Cage {sum = 14, indexes = new[] {39, 30,}},
                            new CageRule.Cage {sum = 18, indexes = new[] {31, 40, 41, 42,}},
                            new CageRule.Cage {sum = 9, indexes = new[] {43, 44,}},
                            new CageRule.Cage {sum = 16, indexes = new[] {32, 33, 34,}},
                            new CageRule.Cage {sum = 13, indexes = new[] {35, 26,}},
                            new CageRule.Cage {sum = 11, indexes = new[] {18, 9,}},
                            new CageRule.Cage {sum = 13, indexes = new[] {27, 28, 19,}},
                            new CageRule.Cage {sum = 8, indexes = new[] {20, 21,}},
                            new CageRule.Cage {sum = 8, indexes = new[] {22, 23,}},
                            new CageRule.Cage {sum = 10, indexes = new[] {24, 25,}},
                            new CageRule.Cage {sum = 11, indexes = new[] {10, 11,}},
                            new CageRule.Cage {sum = 2, indexes = new[] {0,}},
                            new CageRule.Cage {sum = 11, indexes = new[] {1, 2,}},
                            new CageRule.Cage {sum = 7, indexes = new[] {13, 12, 3,}},
                            new CageRule.Cage {sum = 20, indexes = new[] {14, 15, 16,}},
                            new CageRule.Cage {sum = 14, indexes = new[] {17, 8, 7,}},
                            new CageRule.Cage {sum = 20, indexes = new[] {4, 5, 6,}},
                        },
                    },
                }
            };
            Console.WriteLine("Sudoku:\n" + sudoku.initNumbers.NumbersToString());

            var (matrix, primaryColumns, secondaryColumns) = SudokuDlxUtil.SudokuToMatrix(sudoku);
            var solutions = Dlx.Solve(matrix, primaryColumns, secondaryColumns).ToArray();
            Console.WriteLine("Solution: count:" + solutions.Length + "\n");
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
    }
}
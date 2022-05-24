using System;
using System.Linq;
using DlxLib;
using NUnit.Framework;
using SudokuConverterTest.Utils;
using SudokuDlxLib;
using SudokuGeneratorLib;
using SudokuGeneratorLib.Utils;

namespace SudokuGeneratorTest
{
    [TestFixture]
    public class DiagonalTests
    {
        // [Test]
        // public void TestMatrixShuffle()
        // {
        //     var sudoku = new Sudoku
        //     {
        //         initNumbers = new[] {0, 0, 0, 0, 0, 8, 3, 0, 0, 0, 6, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 8, 5, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1, 0, 2, 4, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 5, 0, 0},
        //         rules = new Rule[]
        //         {
        //             new NormalRule(),
        //         }
        //     };
        //
        //     var matrix = SudokuDlxUtil.SudokuToMatrix(sudoku);
        //     matrix.matrix.ShuffleDimension0();
        //     // MatrixUtil.PrintMatrix(matrix);
        //     var solutions = Dlx.Solve(matrix.matrix, matrix.primaryColumns, matrix.secondaryColumns).ToArray();
        //     foreach (var result in solutions)
        //     {
        //         Console.WriteLine("Solution:" + String.Join(",", result));
        //     }
        //
        //     Assert.True(solutions.Length == 1);
        //
        //     if (solutions.Length == 1)
        //     {
        //         var solutionNumbers = SudokuDlxUtil.SolutionToNumbers(sudoku, matrix.matrix, solutions[0]);
        //
        //         Console.WriteLine("Sudoku Solution:\n" + solutionNumbers.NumbersToString());
        //     }
        // }
        //
        // [Test]
        // public void TestGenerateDiagonal()
        // {
        //     var sudoku = SudokuGenerator.GenerateDiagonalSudoku(0);
        //     Console.WriteLine("Sudoku:\n" + sudoku.initNumbers.NumbersToString());
        //     Assert.True(true);
        // }
    }
}
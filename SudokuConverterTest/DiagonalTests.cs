using System;
using NUnit.Framework;
using SudokuConverterLib;
using SudokuGeneratorLib;

namespace SudokuConverterTest
{
    // [TestFixture]
    // public class DiagonalTests
    // {
    //     [Test]
    //     public void TestGenerateDiagonalSudoku1()
    //     {
    //         var count = 10;
    //         for (int i = 0; i < count; i++)
    //         {
    //             var sudoku = SudokuGenerator.GenerateDiagonalSudoku(30, 10);
    //             Console.WriteLine(sudoku.ToDataString());
    //         }
    //
    //         Assert.True(true);
    //     }
    //
    //     [Test]
    //     public void TestGenerateDiagonalSudoku2()
    //     {
    //         var count = 15;
    //         for (int i = 0; i < count; i++)
    //         {
    //             var sudoku = SudokuGenerator.GenerateDiagonalSudoku(40, 10);
    //             Console.WriteLine(sudoku.ToDataString());
    //         }
    //
    //         Assert.True(true);
    //     }
    //
    //     [Test]
    //     public void TestGenerateDiagonalSudoku3()
    //     {
    //         var count = 15;
    //         for (int i = 0; i < count; i++)
    //         {
    //             var sudoku = SudokuGenerator.GenerateDiagonalSudoku(30, 25);
    //             Console.WriteLine(sudoku.ToDataString());
    //         }
    //
    //         Assert.True(true);
    //     }
    //
    //     [Test]
    //     public void TestGenerateDiagonalSudoku4()
    //     {
    //         var count = 10;
    //         for (int i = 0; i < count; i++)
    //         {
    //             var sudoku = SudokuGenerator.GenerateDiagonalSudoku(30, 35);
    //             Console.WriteLine(sudoku.ToDataString());
    //         }
    //
    //         Assert.True(true);
    //     }
    //
    //     [Test]
    //     public void TestGenerateDiagonalSudoku5()
    //     {
    //         var count = 10;
    //         for (int i = 0; i < count; i++)
    //         {
    //             var sudoku = SudokuGenerator.GenerateDiagonalSudoku(25, 50);
    //             Console.WriteLine(sudoku.ToDataString());
    //         }
    //
    //         Assert.True(true);
    //     }
    // }
}
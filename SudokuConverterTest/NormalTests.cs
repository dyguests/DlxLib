using System;
using NUnit.Framework;
using SudokuConverterLib;
using SudokuConverterTest.Utils;
using SudokuGeneratorLib;

namespace SudokuConverterTest
{
    [TestFixture]
    public class NormalTests
    {
        [Test]
        public void TestGenerateNormalSudoku()
        {
            var sudoku = SudokuGenerator.GenerateNormalSudoku(30);
            Console.WriteLine("Sudoku:\n" + sudoku.initNumbers.NumbersToString());
            Console.WriteLine("Sudoku:\n" + sudoku.ToDataString());
            Assert.True(true);
        }

        [Test]
        public void TestGenerateKillerSudoku()
        {
            var sudoku = SudokuGenerator.GenerateKillerSudoku(50, 3, 50);
            Console.WriteLine("Sudoku:\n" + sudoku.initNumbers.NumbersToString());
            Console.WriteLine("Sudoku:\n" + sudoku.ToDataString());
            Assert.True(true);
        }

        [Test]
        public void TestGenerateKillerSudokus1()
        {
            var count = 10;
            for (int i = 0; i < count; i++)
            {
                // var sudoku = SudokuGenerator.GenerateKillerSudoku(50, 3, 50);
                // var sudoku = SudokuGenerator.GenerateKillerSudoku(72, 4, 81);
                var sudoku = SudokuGenerator.GenerateKillerSudoku(81, 5, 50);
                Console.WriteLine(sudoku.ToDataString());
            }

            Assert.True(true);
        }
    }
}
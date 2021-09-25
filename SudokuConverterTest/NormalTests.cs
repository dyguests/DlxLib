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
    }
}
using System;
using NUnit.Framework;
using SudokuConverterTest.Utils;
using SudokuGeneratorLib;

namespace SudokuGeneratorTest
{
    [TestFixture]
    public class DiagonalTests
    {
        [Test]
        public void TestGenerateDiagonal()
        {
            var sudoku = SudokuGenerator.GenerateDiagonalSudoku();
            Console.WriteLine("Sudoku:\n" + sudoku.initNumbers.NumbersToString());
            Assert.True(true);
        }
    }
}
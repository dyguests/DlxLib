using System;
using NUnit.Framework;
using SudokuConverterTest.Utils;
using SudokuGeneratorLib;

namespace SudokuConverterTest
{
    [TestFixture]
    public class NormalTests
    {
        [Test]
        public void Test1()
        {
            var sudoku = SudokuGenerator.GenerateNormalSudoku(30);
            Console.WriteLine("Sudoku:\n" + sudoku.initNumbers.NumbersToString());
            Assert.True(true);
        }
    }
}
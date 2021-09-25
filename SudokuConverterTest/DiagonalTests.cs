using System;
using NUnit.Framework;
using SudokuConverterLib;
using SudokuGeneratorLib;

namespace SudokuConverterTest
{
    [TestFixture]
    public class DiagonalTests
    {
        [Test]
        public void TestGenerateDiagonalSudoku1()
        {
            var count = 10;
            for (int i = 0; i < count; i++)
            {
                var sudoku = SudokuGenerator.GenerateDiagonalSudoku(50);
                Console.WriteLine(sudoku.ToDataString());
            }

            Assert.True(true);
        }
    }
}
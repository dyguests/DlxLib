using System;
using NUnit.Framework;
using SudokuDlxLib.Utils;
using SudokuGeneratorLib;

namespace SudokuGeneratorTest
{
    [TestFixture]
    public class KillerTests
    {
        [Test]
        public void TestGenerateEasy()
        {
            var sudoku = SudokuGenerator.GenerateKillerSudoku(50, 3, 9 * 9);
            Console.WriteLine("Sudoku:\n" + sudoku.initNumbers.NumbersToString());
            Assert.True(true);
        }
    }
}
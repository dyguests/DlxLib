using System;
using NUnit.Framework;
using SudokuDlxLib.Utils;
using SudokuGeneratorLib;

namespace SudokuGeneratorTest
{
    [TestFixture]
    public class NormalTests
    {
        [Test]
        public void TestGenerateSolution()
        {
            var solutionNumbers = SudokuGenerator.GenerateSolution();
            Console.WriteLine("Solution:" + String.Join("", solutionNumbers));
            Assert.True(true);
        }

        [Test]
        public void TestGenerateEasy()
        {
            var sudoku = SudokuGenerator.GenerateNormalSudoku(30);
            Console.WriteLine("Sudoku:\n" + sudoku.initNumbers.NumbersToString());
            Assert.True(true);
        }

        [Test]
        public void TestGenerateM()
        {
            var sudoku = SudokuGenerator.GenerateNormalSudoku(35, 6);
            Console.WriteLine("Sudoku:\n" + sudoku.initNumbers.NumbersToString());
            Assert.True(true);
        }

        [Test]
        public void TestGenerateHard()
        {
            var sudoku = SudokuGenerator.GenerateNormalSudoku(40, 12);
            Console.WriteLine("Sudoku:\n" + sudoku.initNumbers.NumbersToString());
            Assert.True(true);
        }
    }
}
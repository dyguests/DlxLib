using System;
using NUnit.Framework;
using SudokuConverterLib;
using SudokuConverterTest.Utils;
using SudokuGeneratorLib;

namespace SudokuConverterTest
{
    [TestFixture]
    public class KillerTests
    {
        [Test]
        public void TestGenerateKillerSudoku()
        {
            var sudoku = SudokuGenerator.GenerateKillerSudoku(50, 3, 50);
            Console.WriteLine("Sudoku:\n" + sudoku.initNumbers.NumbersToString());
            Console.WriteLine("Sudoku:\n" + sudoku.ToDataString());
            Assert.True(true);
        }

        [Test]
        public void TestGenerateKillerSudoku4()
        {
            var sudoku = SudokuGenerator.GenerateKillerSudoku(81, 6, 40);
            // var sudoku = SudokuGenerator.GenerateKillerSudoku(81, 5, 50);
            Console.WriteLine(sudoku.ToDataString());

            Assert.True(true);
        }

        [Test]
        public void TestGenerateKillerSudokus1()
        {
            var count = 15;
            for (int i = 0; i < count; i++)
            {
                var sudoku = SudokuGenerator.GenerateKillerSudoku(50, 3, 70);
                Console.WriteLine(sudoku.ToDataString());
            }

            Assert.True(true);
        }

        [Test]
        public void TestGenerateKillerSudokus2()
        {
            var count = 15;
            for (int i = 0; i < count; i++)
            {
                var sudoku = SudokuGenerator.GenerateKillerSudoku(63, 4, 81);
                // var sudoku = SudokuGenerator.GenerateKillerSudoku(81, 5, 50);
                Console.WriteLine(sudoku.ToDataString());
            }

            Assert.True(true);
        }

        [Test]
        public void TestGenerateKillerSudokus3()
        {
            var count = 10;
            for (int i = 0; i < count; i++)
            {
                var sudoku = SudokuGenerator.GenerateKillerSudoku(63, 5, 60);
                // var sudoku = SudokuGenerator.GenerateKillerSudoku(81, 5, 50);
                Console.WriteLine(sudoku.ToDataString());
            }

            Assert.True(true);
        }

        [Test]
        public void TestGenerateKillerSudokus4()
        {
            var count = 1;
            for (int i = 0; i < count; i++)
            {
                // 生成时间太久了，半小时都完成不了一关
                var sudoku = SudokuGenerator.GenerateKillerSudoku(81, 6, 45);
                // var sudoku = SudokuGenerator.GenerateKillerSudoku(81, 5, 50);
                Console.WriteLine(sudoku.ToDataString());
            }

            Assert.True(true);
        }
    }
}
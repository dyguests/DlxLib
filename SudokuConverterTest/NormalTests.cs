using System;
using NUnit.Framework;
using SudokuConverterLib;
using SudokuGeneratorLib;

namespace SudokuConverterTest
{
    [TestFixture]
    public class NormalTests
    {
        // [Test]
        public void TestGenerateNormalSudoku()
        {
            var sudoku = SudokuGenerator.GenerateNormalSudoku(30);
            Console.WriteLine(sudoku.ToDataString());
            Assert.True(true);
        }

        [Test]
        public void TestGenerateNormalSudoku0()
        {
            for (int i = 0; i < 10; i++)
            {
                var sudoku = SudokuGenerator.GenerateNormalSudoku(20);
                Console.WriteLine(sudoku.ToDataString());
                Assert.True(true);
            }
        }

        [Test]
        public void TestGenerateNormalSudoku1()
        {
            for (int i = 0; i < 10; i++)
            {
                var sudoku = SudokuGenerator.GenerateNormalSudoku(30);
                Console.WriteLine(sudoku.ToDataString());
                Assert.True(true);
            }
        }

        [Test]
        public void TestGenerateNormalSudoku2()
        {
            for (int i = 0; i < 150; i++)
            {
                var sudoku = SudokuGenerator.GenerateNormalSudoku(35, 5);
                Console.WriteLine(sudoku.ToDataString());
                Assert.True(true);
            }
        }

        [Test]
        public void TestGenerateNormalSudoku3()
        {
            for (int i = 0; i < 150; i++)
            {
                var sudoku = SudokuGenerator.GenerateNormalSudoku(40, 10);
                Console.WriteLine(sudoku.ToDataString());
                Assert.True(true);
            }
        }

        [Test]
        public void TestGenerateNormalSudoku4()
        {
            for (int i = 0; i < 150; i++)
            {
                var sudoku = SudokuGenerator.GenerateNormalSudoku(45, 15);
                Console.WriteLine(sudoku.ToDataString());
                Assert.True(true);
            }
        }
    }
}
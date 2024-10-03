using System;
using NUnit.Framework;
using SudokuDlxLib;
using SudokuLib;
using SudokuLibTest;

namespace SudokuDlxLibTest
{
    public class SudokuDlxUtilTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void Test1()
        {
            const string sketch = "...........91......1..5.36..9..82..7.4..35..2..27..63..5.....4...1...9.....421...";
            var puzzle = PuzzleSketcher.FromSketch(sketch);
            Console.WriteLine($"sketch:{sketch}");
            Console.WriteLine($"puzzle:\n{puzzle.ToDisplay()}");
            int[,] matrix = SudokuDlxUtil.ToMatrix(puzzle);
            Assert.Pass();
        }
    }
}
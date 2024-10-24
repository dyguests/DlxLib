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
        public void TestPuzzleWithSolution()
        {
            const string sketch = "32586719486914327571425936819368245764793581258271463925839674143157892697642158c";
            TestPuzzle(sketch);
        }

        [Test]
        public void TestPuzzleX()
        {
            // ........1..2...........34......5.......2...3.....6.........47....6..............8

            const string sketch = @"........1..2...........34......5.......2...3.....6.........47....6..............8
Diagonal";
            TestPuzzle(sketch);
        }

        private static void TestPuzzle(string sketch)
        {
            var puzzle = PuzzleSketcher.FromSketch(sketch);
            Console.WriteLine($"sketch:\n{sketch}");
            Console.WriteLine($"puzzle:\n{puzzle.ToDisplay()}");
            var dlx = SudokuDlxUtil.ToDlx(puzzle);
            foreach (var result in dlx.Solve())
            {
                Console.WriteLine("dlx Solution:" + string.Join(",", result));
                var solution = SudokuDlxUtil.ToSolution(puzzle, dlx.ReadonlyMatrix, result);
                puzzle.SetSolution(solution);
                Console.WriteLine($"sudoku Solution:\n{solution.DigitsToDisplay()}");
            }

            var sketch2 = PuzzleSketcher.ToSketch(puzzle, useMask: false);
            Console.WriteLine($"solution sketch:\n{sketch2}");
            Assert.Pass();
        }
    }
}
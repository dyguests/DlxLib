using System;
using DlxLib;
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
        public void TestPuzzleOnly()
        {
            const string sketch = "...........91......1..5.36..9..82..7.4..35..2..27..63..5.....4...1...9.....421...";
            var puzzle = PuzzleSketcher.FromSketch(sketch);
            Console.WriteLine($"sketch:\n{sketch}");
            Console.WriteLine($"puzzle:\n{puzzle.ToDisplay()}");
            var matrix = SudokuDlxUtil.ToMatrix(puzzle);
            // Console.WriteLine($"matrix:\n{matrix.ToDisplay()}");
            var dlx = new Dlx(matrix);
            foreach (var result in dlx.Solve())
            {
                Console.WriteLine("dlx Solution:" + string.Join(",", result));
                var solution = SudokuDlxUtil.ToSolution(puzzle, matrix, result);
                puzzle.SetSolution(solution);
                Console.WriteLine($"sudoku Solution:\n{solution.DigitsToDisplay()}");
            }

            var sketch2 = PuzzleSketcher.ToSketch(puzzle, useMask: false);
            Console.WriteLine($"solution sketch:\n{sketch2}");
            Assert.Pass();
        }

        [Test]
        public void TestPuzzleWithSolution()
        {
            const string sketch = "32586719486914327571425936819368245764793581258271463925839674143157892697642158c";
            TestPuzzle(sketch);
        }

        [Test]
        public void TestPuzzleX()
        {
            // 123456789789123456456789231637218945918534267542697813264973581875146392295861374
            // 12345678978912345645678923163721894591853426754269781326497358187514639229586137.
            // ........1..2...........34......5.......2...3.....6.........47....6..............8

             const string sketch = @"12345678978912345645678923163721894591853426754269781326497358187514639229586137.
Diagonal";
            TestPuzzle(sketch);
        }

        private static void TestPuzzle(string sketch)
        {
            var puzzle = PuzzleSketcher.FromSketch(sketch);
            Console.WriteLine($"sketch:\n{sketch}");
            Console.WriteLine($"puzzle:\n{puzzle.ToDisplay()}");
            var matrix = SudokuDlxUtil.ToMatrix(puzzle);
            // Console.WriteLine($"matrix:\n{matrix.ToDisplay()}");
            var dlx = new Dlx(matrix);
            foreach (var result in dlx.Solve())
            {
                Console.WriteLine("dlx Solution:" + string.Join(",", result));
                var solution = SudokuDlxUtil.ToSolution(puzzle, matrix, result);
                puzzle.SetSolution(solution);
                Console.WriteLine($"sudoku Solution:\n{solution.DigitsToDisplay()}");
            }

            var sketch2 = PuzzleSketcher.ToSketch(puzzle, useMask: false);
            Console.WriteLine($"solution sketch:\n{sketch2}");
            Assert.Pass();
        }
    }
}
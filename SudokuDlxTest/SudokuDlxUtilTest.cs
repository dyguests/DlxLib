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
        public void TestPuzzle2()
        {
            const string sketch = "........3.7.....5...418.2....1..6.....7..5.48...41..62..5.......6..32...3...91...";
            //                     186257493273964851954183276421876935697325148538419762715648329869732514342591687
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

        [Test]
        public void TestPuzzleKiller()
        {
            const string sketch = @"...867194869143275714259368193682457647935812582714639258396741431578926976421...
Killer 5=0+1;11=79+80";
            //                    "325867194869143275714259368193682457647935812582714639258396741431578926976421583";
            TestPuzzle(sketch);
        }

        [Test]
        public void TestPuzzleKiller2()
        {
            const string sketch = @"........3.7.....5...418.2....1..6.....7..5.48...41..62..5.......6..32...3...9....
Killer 10=76+77";
            //                    "325867194869143275714259368193682457647935812582714639258396741431578926976421583";
            TestPuzzle(sketch);
        }

        [Test]
        public void TestWorkshopKiller1()
        {
            const string sketch = @".................................................................................
Killer 20=0+1+2";
            //                    "325867194869143275714259368193682457647935812582714639258396741431578926976421583";
            TestPuzzle(sketch);
            
            Assert.Pass();
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
using System;
using System.Linq;
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

        [Test]
        public void TestWorkshopKiller2()
        {
            const string sketch = @".................................................................................
Killer 20=0+1+2;9=3+4";
            //                    "325867194869143275714259368193682457647935812582714639258396741431578926976421583";
            TestPuzzle(sketch);

            Assert.Pass();
        }

        [Test]
        public void TestKillCage0Sum()
        {
            const string sketch = @".................................................................................
Killer 0=0+1";
            TestPuzzle(sketch);

            Assert.Pass();
        }

        [Test]
        public void TestKillCage0Sum2()
        {
            const string sketch = @".................................................................................
Killer 45=10+11+12+19+20+21+28+29+30";
            TestPuzzle(sketch);

            Assert.Pass();
        }

        [Test]
        public void TestKillCage0Sum3()
        {
            const string sketch = @".................................................................................
Killer 0=10+11+12+19+20+21+28+29+30";
            TestPuzzle(sketch);

            Assert.Pass();
        }

        [Test]
        public void TestKillCage0Sum02()
        {
            const string sketch = @".................................................................................
Killer 0=0+1";
            TestPuzzle(sketch);

            Assert.Pass();
        }

        [Test]
        public void TestKillCage0Sum03()
        {
            const string sketch = @".................................................................................
Killer 0=0+1+2";
            TestPuzzle(sketch);

            Assert.Pass();
        }

        [Test]
        public void TestKillCage0Sum04()
        {
            const string sketch = @".................................................................................
Killer 0=0+1+2+3";
            TestPuzzle(sketch);

            Assert.Pass();
        }

        [Test]
        public void TestKillCage0Sum07()
        {
            const string sketch = @".................................................................................
Killer 0=0+1+2+3+4+5+6";
            TestPuzzle(sketch);

            Assert.Pass();
        }

        [Test]
        public void TestKillCage0Sum08()
        {
            const string sketch = @".................................................................................
Killer 0=0+1+2+3+4+5+6+7";
            TestPuzzle(sketch);

            Assert.Pass();
        }

        [Test]
        public void TestKillCage0Sum09()
        {
            const string sketch = @".................................................................................
Killer 0=0+1+2+3+4+5+6+7+8";
            TestPuzzle(sketch);

            Assert.Pass();
        }

        [Test]
        public void TestKillCage0Sum092()
        {
            const string sketch = @".................................................................................
Killer 0=9+10+11+18+19+20+27+28+29";
            TestPuzzle(sketch);

            Assert.Pass();
        }

        [Test]
        public void TestKillCage0Sum093()
        {
            const string sketch = @".................................................................................
Killer 0=10+11+12+19+20+21+28+29+30"; // todo 这个，就是没解？
            TestPuzzle(sketch);

            Assert.Pass();
        }

        [Test]
        public void Test2x2()
        {
            const string sketch = @"....";
            TestPuzzle(sketch);

            Assert.Pass();
        }

        private static void TestPuzzle(string sketch)
        {
            var puzzle = PuzzleSketcher.FromSketch(sketch);
            Console.WriteLine($"sketch:\n{sketch}");
            Console.WriteLine($"puzzle:\n{puzzle.ToDisplay()}");
            var dlx = SudokuDlxUtil.ToDlx(puzzle);
            // dlx.Display();
            foreach (var dlxSolution in dlx.Solve())
            {
                Console.WriteLine("dlx Solution:" + string.Join(",", dlxSolution.RowIndexes));

                if (false)
                {
                    Console.WriteLine("dlx Solution rows:");
                    foreach (var row in dlx.ReadonlyMatrix.MatrixToRows().Where((row, index) => Array.IndexOf(dlxSolution.RowIndexes, index) >= 0))
                    {
                        Console.WriteLine(string.Join(",", row));
                    }

                    var mergeRow = dlx.ReadonlyMatrix.MatrixToRows()
                        .Where((row, index) => Array.IndexOf(dlxSolution.RowIndexes, index) >= 0)
                        .Aggregate((a, b) =>
                        {
                            var result = new int[a.Length];
                            for (var i = 0; i < a.Length; i++)
                            {
                                result[i] = a[i] + b[i];
                            }

                            return result;
                        });
                    Console.WriteLine($"mergeRow:\n{string.Join(",", mergeRow)}");
                }

                Console.WriteLine($"dlx deep:{dlxSolution.Deep} step:{dlxSolution.Step}");
                var solution = SudokuDlxUtil.ToSolution(puzzle, dlx.ReadonlyMatrix, dlxSolution.RowIndexes);
                puzzle.SetSolution(solution);
                Console.WriteLine($"sudoku Solution:\n{solution.DigitsToDisplay(puzzle.Size)}");
            }

            var sketch2 = PuzzleSketcher.ToSketch(puzzle, useMask: false);
            Console.WriteLine($"solution sketch:\n{sketch2}");
            Assert.Pass();
        }
    }
}
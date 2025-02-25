using System;
using System.Linq;
using SudokuDlxLib;
using SudokuLib;

namespace SudokuGeneratorLib
{
    public static class SudokuGenerator
    {
        public static IPuzzle GenerateRandomFill()
        {
            var puzzle = new Puzzle(new int[9 * 9]);
            // Console.WriteLine($"puzzle:\n{puzzle.ToDisplay()}");
            var dlx = SudokuDlxUtil.ToDlx(puzzle, ExpandRowType.Random);
            var result = dlx.Solve().Take(1).FirstOrDefault().RowIndexes;
            if (result == null) return null;
            // Console.WriteLine("dlx Solution:" + string.Join(",", result));
            var solution = SudokuDlxUtil.ToSolution(puzzle, dlx, result);
            Array.Copy(solution, puzzle.Digits, solution.Length);
            puzzle.SetSolution(solution);
            // Console.WriteLine($"puzzle:\n{puzzle.ToDisplay()}");
            // var sketch2 = PuzzleSketcher.ToSketch(puzzle, useMask: false);
            // Console.WriteLine($"solution sketch:\n{sketch2}");
            return puzzle;
        }
    }
}
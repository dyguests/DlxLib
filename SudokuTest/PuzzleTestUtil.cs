using System;
using System.Linq;
using SudokuLib;

namespace SudokuLibTest
{
    public static class PuzzleTestUtil
    {
        public static string ToDisplay(this IPuzzle puzzle)
        {
            return $"Digits:\n{DigitsToDisplay(puzzle.Digits, puzzle.Size)}\n"
                   + $"Solution:\n{DigitsToDisplay(puzzle.Solution, puzzle.Size)}";
        }

        public static string DigitsToDisplay(this int[] digits, int[] size)
        {
            return digits
                .Select((digit, index) => new { digit, index })
                .GroupBy(x => x.index / size[0], x => x.digit)
                .Select(group => string.Join(" ", group))
                .Aggregate((current, next) => current + Environment.NewLine + next);
        }
    }
}
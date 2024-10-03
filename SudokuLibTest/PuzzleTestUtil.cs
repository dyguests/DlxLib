using System;
using System.Linq;
using SudokuLib;

namespace SudokuLibTest
{
    public static class PuzzleTestUtil
    {
        public static string ToDisplay(this IPuzzle puzzle)
        {
            return $"Digits:\n{DigitsToDisplay(puzzle.Digits)}\n"
                   + $"Solution:\n{DigitsToDisplay(puzzle.Solution)}";
        }

        public static string DigitsToDisplay(this int[] digits)
        {
            return digits
                .Select((digit, index) => new { digit, index })
                .GroupBy(x => x.index / 9, x => x.digit)
                .Select(group => string.Join(" ", group))
                .Aggregate((current, next) => current + Environment.NewLine + next);
        }
    }
}
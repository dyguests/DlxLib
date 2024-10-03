using System;
using System.Linq;
using SudokuLib;

namespace SudokuLibTest
{
    public static class PuzzleTestUtil
    {
        public static string ToDisplay(this IPuzzle puzzle)
        {
            return puzzle.Digits
                .Select((digit, index) => new { digit, index })
                .GroupBy(x => x.index / 9, x => x.digit)
                .Select(group => string.Join(" ", group))
                .Aggregate((current, next) => current + Environment.NewLine + next);
        }
    }
}
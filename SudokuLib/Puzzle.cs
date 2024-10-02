using System;

namespace SudokuLib
{
    public interface IPuzzle
    {
        int[] Digits { get; }
        int[] Solution { get; }
    }

    public class Puzzle : IPuzzle
    {
        public int[] Digits { get; }
        public int[] Solution { get; }

        public Puzzle(int[] digits)
        {
            if (digits == null || digits.Length == 0)
            {
                throw new ArgumentException("Digits must be initialized", nameof(digits));
            }

            Digits = digits;
            Solution = new int[digits.Length];
        }
    }
}
using System;
using System.Linq;
using SudokuLib.Rules;

namespace SudokuLib
{
    public interface IPuzzle
    {
        int[] Digits { get; }
        int[] Solution { get; }
        IRule[] Rules { get; }
    }

    public class Puzzle : IPuzzle
    {
        public int[] Digits { get; }
        public int[] Solution { get; }
        public IRule[] Rules { get; }

        public Puzzle(int[] digits, params IRule[] rules)
        {
            if (digits == null || digits.Length == 0)
            {
                throw new ArgumentException("Digits must be initialized", nameof(digits));
            }

            Digits = digits;
            Solution = new int[digits.Length];
            Rules = rules.Any(rule => rule is IBaseRule) ? rules : new[] { new StandardRule() }.Concat(rules).ToArray();
        }
    }
}
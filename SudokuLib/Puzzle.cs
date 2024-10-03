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

        void SetSolution(int[] solution);
    }

    public class Puzzle : IPuzzle
    {
        #region IPuzzle

        public int[] Digits { get; }
        public int[] Solution { get; }
        public IRule[] Rules { get; }

        public void SetSolution(int[] solution)
        {
            if (solution == null || solution.Length != Digits.Length)
            {
                throw new ArgumentException("Solution must be initialized and have the same length as Digits", nameof(solution));
            }

            Array.Copy(solution, Solution, solution.Length);
        }

        #endregion

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
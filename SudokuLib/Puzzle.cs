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

        /// <summary>
        /// Gets the dimensions of the puzzle grid where Size[0] is width and Size[1] is height
        ///
        /// 暂时仅支持正方形
        /// </summary>
        int[] Size { get; }

        void SetSolution(int[] solution);
        void SetRules(IRule[] rules);
    }

    public class Puzzle : IPuzzle
    {
        #region IPuzzle

        public int[] Digits { get; }
        public int[] Solution { get; }
        public IRule[] Rules { get; private set; } = null!;
        public int[] Size { get; private set; }

        public void SetSolution(int[] solution)
        {
            if (solution == null || solution.Length != Digits.Length)
            {
                throw new ArgumentException("Solution must be initialized and have the same length as Digits", nameof(solution));
            }

            Array.Copy(solution, Solution, solution.Length);
        }

        public void SetRules(IRule[] rules)
        {
            var filteredRules = rules.Where(rule => rule != null).Cast<IRule>().ToArray();
            Rules = filteredRules.Any(rule => rule is IBaseRule)
                ? filteredRules
                : new[] { new StandardRule() }.Concat(filteredRules).ToArray();
        }

        private static IRule[] FilterNotNull(IRule[] rules)
        {
            return rules?.Where(rule => rule != null).Cast<IRule>().ToArray() ?? Array.Empty<IRule>();
        }

        #endregion

        public Puzzle(int[] digits, params IRule[] rules) : this(digits, (int)Math.Sqrt(digits.Length), rules)
        {
            if (digits == null || digits.Length == 0)
            {
                throw new ArgumentException("Digits must be initialized", nameof(digits));
            }
        }

        public Puzzle(int[] digits, int size, params IRule[] rules)
            : this(digits, new[] { size, size }, rules)
        {
            if (size <= 0)
            {
                throw new ArgumentException("Size must be positive", nameof(size));
            }
        }

        public Puzzle(int[] digits, int[] size, params IRule[] rules)
        {
            if (size == null || size.Length != 2)
            {
                throw new ArgumentException("Size must be an array of length 2", nameof(size));
            }

            if (size[0] <= 0 || size[1] <= 0)
            {
                throw new ArgumentException("Size dimensions must be positive", nameof(size));
            }

            if (digits == null || digits.Length != size[0] * size[1])
            {
                throw new ArgumentException($"Digits must be initialized and have length of {size[0] * size[1]}", nameof(digits));
            }

            Digits = digits;
            Solution = new int[digits.Length];
            Size = size;
            SetRules(FilterNotNull(rules));
        }
    }
}
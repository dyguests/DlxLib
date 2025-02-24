using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SudokuLib.Rules;

namespace SudokuLib
{
    public static class PuzzleSketcher
    {
        private const char CharDigit1 = '1';
        private const char CharDigit9 = '9';
        private const char CharMask1 = 'a';
        private const char CharMask9 = 'i';

        // todo 这里 创建 实例，然后再 FromSketch 是否不符合规范？？！
        private static readonly IRule[] RuleSketchers =
        {
            DiagonalRule.Default,
            KillerRule.Default,
        };

        /// <summary>
        /// 注sketch不一定是9x9
        /// </summary>
        /// <param name="sketch"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IPuzzle FromSketch(string sketch)
        {
            var lines = sketch.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).Let(strings => new Queue<string>(strings));

            var digitsSketch = lines.Let(queue =>
            {
                // 不断从队列中 Dequeue，直到找到非空字符串为止
                while (queue.Count > 0)
                {
                    var dequeue = queue.Dequeue();
                    if (string.IsNullOrWhiteSpace(dequeue)) continue;
                    return dequeue.Trim(); // 找到非空字符串时返回
                }

                // 如果队列中全是空，返回 null
                return null;
            }) ?? throw new Exception("sketch is empty");

            var length = digitsSketch.Length;

            var digits = new int[length];
            var solution = new int[length];

            for (var i = 0; i < digitsSketch.Length; i++)
            {
                var c = digitsSketch[i];
                if (c is >= CharDigit1 and <= CharDigit9)
                {
                    digits[i] = c - CharDigit1 + 1;
                    solution[i] = c - CharDigit1 + 1;
                }
                else if (c is >= CharMask1 and <= CharMask9)
                {
                    solution[i] = c - CharMask1 + 1;
                }
            }

            var rules = lines.Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => line.Trim())
                .Select(line =>
                    RuleSketchers.Select(rs => rs.FromSketch(line))
                        .FirstOrDefault(result => result != null)
                )
                .Where(rule => rule != null)
                .ToArray();

            var puzzle = new Puzzle(digits, rules);
            puzzle.SetSolution(solution);
            return puzzle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="puzzle"></param>
        /// <param name="showSolution"></param>
        /// <param name="useMask">是否加一层蒙层；是否区分迷题和迷底；是否区分digits/solution</param>
        /// <returns></returns>
        public static string ToSketch(IPuzzle puzzle, bool showSolution = true, bool useMask = true)
        {
            var sb = new StringBuilder();

            var enumerable = Enumerable.Range(0, puzzle.Digits.Length).Select(i =>
            {
                if (puzzle.Digits[i] != 0) return (char)(CharDigit1 + (puzzle.Digits[i] - 1));
                if (showSolution && puzzle.Solution[i] != 0)
                {
                    return (char)((useMask ? CharMask1 : CharDigit1) + (puzzle.Solution[i] - 1));
                }

                return '.';
            });
            var digitSketch = string.Join("", enumerable);
            sb.Append(digitSketch);

            var rules = puzzle.Rules.Where(rule => rule is not IBaseRule).ToArray();
            foreach (var rule in rules)
            {
                var ruleSketch = rule.ToSketch();
                sb.AppendLine().Append(ruleSketch);
            }

            return sb.ToString();
        }
    }
}
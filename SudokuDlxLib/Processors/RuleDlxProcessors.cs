using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SudokuDlxLib.Utils;
using SudokuLib;
using SudokuTest.Utils;

namespace SudokuDlxLib.Processors
{
    public static class RuleRouter
    {
        public static RuleDlxProcessor GetRuleDlxProcessor(RuleType ruleType)
        {
            switch (ruleType)
            {
                case RuleType.Normal:
                    return new NormalRuleDlxProcessor();
                default:
                    throw new InvalidDataException();
            }
        }
    }

    public abstract class RuleDlxProcessor
    {
        public abstract RuleMatrix RuleToMatrix(Sudoku sudoku, Rule rule);

        public abstract int[] SolutionToNumbers(int[,] matrix, int[] solution);
    }

    public class NormalRuleDlxProcessor : RuleDlxProcessor
    {
        private const int tileCount = 9 * 9;
        private const int rowCount = 9 * 9;
        private const int colCount = 9 * 9;
        private const int boxCount = 9 * 9;

        public override RuleMatrix RuleToMatrix(Sudoku sudoku, Rule rule)
        {
            return new RuleMatrix
            {
                type = RuleType.Normal,
                matrix = ToMatrix(sudoku.initNumbers),
            };
        }

        public override int[] SolutionToNumbers(int[,] matrix, int[] solution)
        {
            return solution.Select(matrix.GetRow)
                .Select(ParseToNumberAndIndex)
                .OrderBy(numberAndIndex => numberAndIndex.Item2)
                .Select(numberAndIndex => numberAndIndex.Item1)
                .ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numbers">init numbers</param>
        /// <returns></returns>
        private int[,] ToMatrix(int[] numbers)
        {
            var rows = numbers.Select((number, index) => GetPossibleNumbers(numbers, index))
                .Select(GenerateRow)
                .SelectMany(rows1 => rows1);
            return ArrayUtil.To2DArray(rows);
        }

        private IEnumerable<int> GetPossibleNumbers(int[] numbers, int index)
        {
            {
                var number = numbers[index];
                if (number > 0)
                {
                    return new[] {number};
                }
            }

            var possibleNumbers = Enumerable.Range(1, 9).Except(
                numbers.Where((number, index1) =>
                    index1 % 9 == index % 9
                    || index1 / 9 == index / 9
                    || (index1 % 9 / 3 == index % 9 / 3 && index1 / 9 / 3 == index / 9 / 3)
                )
            );

            return possibleNumbers;
        }

        private IEnumerable<int[]> GenerateRow(IEnumerable<int> possibleNumbers, int index) => possibleNumbers.Select(possibleNumber =>
        {
            var row = new int[tileCount + rowCount + colCount + boxCount];
            row[index] = 1;
            row[tileCount + index / 9 * 9 + possibleNumber - 1] = 1;
            row[tileCount + rowCount + index % 9 * 9 + possibleNumber - 1] = 1;
            row[tileCount + rowCount + colCount + (index % 9 / 3 + index / 9 / 3 * 3) * 9 + possibleNumber - 1] = 1;
            return row;
        });

        private static Tuple<int, int> ParseToNumberAndIndex(int[] row)
        {
            var index = Array.IndexOf(row, 1, 0, tileCount);
            int number = Array.IndexOf(row, 1, tileCount, rowCount) % 9 + 1;
            return new Tuple<int, int>(number, index);
        }
    }
}
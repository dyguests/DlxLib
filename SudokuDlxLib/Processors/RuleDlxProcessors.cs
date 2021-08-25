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
        private const int TileCount = 9 * 9;
        private const int NumberCount = 9;
        private const int RowCount = 9 * 9;
        private const int ColCount = 9 * 9;
        private const int BoxCount = 9 * 9;

        private static readonly int[] PrimaryColumns = new int[TileCount + RowCount + ColCount + BoxCount];

        static NormalRuleDlxProcessor()
        {
            for (var i = 0; i < PrimaryColumns.Length; i++)
            {
                if (i < TileCount)
                {
                    PrimaryColumns[i] = i;
                }
                else
                {
                    PrimaryColumns[i] = NumberCount + i;
                }
            }
        }

        public override RuleMatrix RuleToMatrix(Sudoku sudoku, Rule rule)
        {
            (int[,] matrix, int[] primaryColumns, int[] secondaryColumns) = ToMatrix(sudoku.initNumbers);
            return new RuleMatrix
            {
                type = RuleType.Normal,
                matrix = matrix,
                primaryColumns = primaryColumns,
                secondaryColumns = secondaryColumns,
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
        /// <returns>
        /// matrix 矩阵
        /// primaryColumns 主列
        /// secondaryColumns 副列
        /// </returns>
        private (int[,] matrix, int[] primaryColumns, int[] secondaryColumns) ToMatrix(int[] numbers)
        {
            var rows = numbers.Select((number, index) => GetPossibleNumbers(numbers, index))
                .Select(GenerateRow)
                .SelectMany(rows1 => rows1);
            return (ArrayUtil.To2DArray(rows), PrimaryColumns, new int[0]);
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
            var row = new int[TileCount + NumberCount + RowCount + ColCount + BoxCount];
            row[index] = 1; //index
            row[TileCount + possibleNumber - 1] = 1; //number
            row[TileCount + NumberCount + index / 9 * 9 + possibleNumber - 1] = 1; //row
            row[TileCount + NumberCount + RowCount + index % 9 * 9 + possibleNumber - 1] = 1; //col
            row[TileCount + NumberCount + RowCount + ColCount + (index % 9 / 3 + index / 9 / 3 * 3) * 9 + possibleNumber - 1] = 1; //box
            return row;
        });

        private static Tuple<int, int> ParseToNumberAndIndex(int[] row)
        {
            var index = Array.IndexOf(row, 1, 0, TileCount);
            int number = Array.IndexOf(row, 1, TileCount, RowCount) % 9 + 1;
            return new Tuple<int, int>(number, index);
        }
    }
}
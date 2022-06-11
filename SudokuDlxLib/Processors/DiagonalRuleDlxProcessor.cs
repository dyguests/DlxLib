using System.Collections.Generic;
using System.Linq;
using SudokuDlxLib.Utils;
using PuzzleLib;
using PuzzleLib.Rules;

namespace SudokuDlxLib.Processors
{
    public class DiagonalRuleDlxProcessor : RuleDlxProcessor
    {
        /// <summary>
        /// 斜杠
        /// </summary>
        private const int SlashCount = 9;

        /// <summary>
        /// 反斜杠
        /// </summary>
        private const int BackslashCount = 9;

        private static readonly int[] PrimaryColumns = new int[TileCount + SlashCount + BackslashCount];

        static DiagonalRuleDlxProcessor()
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

        public override void ReducePossibleNumbers(Sudoku sudoku, int[][] possibleNumbersIndexes)
        {
            for (var i = 0; i < possibleNumbersIndexes.Length; i++)
            {
                possibleNumbersIndexes[i] = possibleNumbersIndexes[i].Intersect(GetPossibleNumbers(sudoku.initNumbers, possibleNumbersIndexes[i], i)).ToArray();
            }
        }

        public override Matrix RuleToMatrix(Sudoku sudoku, int[][] possibleNumbersIndexes)
        {
            (int[,] matrix, int[] primaryColumns, int[] secondaryColumns) = ToMatrix(possibleNumbersIndexes);

            // Console.WriteLine("matrix:\n" + matrix.MatrixToString());
            // Console.WriteLine("primaryColumns:\n" + primaryColumns?.ArrayToString());
            // Console.WriteLine("secondaryColumns:\n" + secondaryColumns?.ArrayToString());

            return new Matrix
            {
                matrix = matrix,
                primaryColumns = primaryColumns,
                secondaryColumns = secondaryColumns,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="possibleNumbersIndexes">init numbers</param>
        /// <returns>
        /// matrix 矩阵
        /// primaryColumns 主列
        /// secondaryColumns 副列
        /// </returns>
        private (int[,] matrix, int[] primaryColumns, int[] secondaryColumns) ToMatrix(int[][] possibleNumbersIndexes)
        {
            var rows = possibleNumbersIndexes.Select(GenerateRow).SelectMany(rows1 => rows1);
            return (ArrayUtil.To2DArray(rows), PrimaryColumns, new int[0]);
        }

        private int[] GetPossibleNumbers(int[] numbers, int[] possibleNumbers, int index)
        {
            {
                var number = numbers[index];
                if (number > 0)
                {
                    return new[] {number};
                }
            }

            if (index % 9 == index / 9)
            {
                possibleNumbers = possibleNumbers.Except(numbers.Where((number, tIndex) => tIndex % 9 == tIndex / 9)).ToArray();
            }

            if (index % 9 + index / 9 == 8)
            {
                possibleNumbers = possibleNumbers.Except(numbers.Where((number, tIndex) => tIndex % 9 + tIndex / 9 == 8)).ToArray();
            }

            return possibleNumbers;
        }

        private IEnumerable<int[]> GenerateRow(IEnumerable<int> possibleNumbers, int index) => possibleNumbers.Select(possibleNumber =>
        {
            var row = new int[TileCount + NumberCount + SlashCount + BackslashCount];
            row[index] = 1; //index
            row[TileCount + possibleNumber - 1] = 1; //number
            if (index % 9 == index / 9)
            {
                row[TileCount + NumberCount + possibleNumber - 1] = 1; //slash
            }

            if (index % 9 + index / 9 == 8)
            {
                row[TileCount + NumberCount + SlashCount + possibleNumber - 1] = 1; //backslash
            }

            return row;
        });
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using SudokuLib;

namespace SudokuDlxLib
{
    public static class SudokuDlxUtil
    {
        #region ToMatrix

        public static int[,] ToMatrix(IPuzzle puzzle)
        {
            var rows = puzzle.Rules.Aggregate(CreatePositionRows(puzzle.Digits.Length), (current, rule) => rule.ExpandRows(current, puzzle));
            return RowsToMatrix(rows);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns>按position升序的IEnumerable</returns>
        private static IEnumerable<int[]> CreatePositionRows(int size)
        {
            return Enumerable.Range(0, size).Select(i =>
            {
                var row = new int[size];
                row[i] = 1;
                return row;
            });
        }

        private static int[,] RowsToMatrix(IEnumerable<int[]> rows)
        {
            var rowList = rows.ToList();
            var matrix = new int[rowList.Count, rowList[0].Length];
            for (var i = 0; i < rowList.Count; i++)
            {
                for (var j = 0; j < rowList[i].Length; j++)
                {
                    matrix[i, j] = rowList[i][j];
                }
            }

            return matrix;
        }

        #endregion

        #region ToSolution

        public static int[] ToSolution(IPuzzle puzzle, int[,] matrix, int[] dlxResult)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
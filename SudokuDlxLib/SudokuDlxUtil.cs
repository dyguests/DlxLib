using System.Collections.Generic;
using System.Linq;
using DlxLib;
using SudokuLib;

namespace SudokuDlxLib
{
    public static class SudokuDlxUtil
    {
        #region ToMatrix

        public static Dlx ToDlx(IPuzzle puzzle)
        {
            // 0:主列 1:副列 2:提示列
            var rows = CreatePositionRows(puzzle.Digits.Length);
            var columnPredicate = new int[puzzle.Digits.Length];
            foreach (var rule in puzzle.Rules)
            {
                (rows, columnPredicate) = rule.ExpandRows(rows, columnPredicate, puzzle);
            }

            return new Dlx(rows.RowsToMatrix(), columnPredicate);
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

        #endregion

        #region ToSolution

        public static int[] ToSolution(IPuzzle puzzle, int[,] matrix, int[] rowIndexes)
        {
            var solution = (int[])puzzle.Digits.Clone();
            var rows = matrix.MatrixToRows().ToList();
            var resultRows = rowIndexes.Select(rowIndex => rows[rowIndex]).ToList();
            foreach (var rule in puzzle.Rules)
            {
                if (rule.FillSolution(solution, resultRows, puzzle)) break;
            }

            return solution;
        }

        #endregion
    }
}
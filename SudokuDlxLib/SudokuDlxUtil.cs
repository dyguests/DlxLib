using System;
using System.Collections.Generic;
using System.Linq;
using DlxLib;
using SudokuDlxLib.Rules;
using SudokuDlxLib.Utils;
using SudokuLib;

namespace SudokuDlxLib
{
    public static class SudokuDlxUtil
    {
        #region ToMatrix

        public static Dlx ToDlx(IPuzzle puzzle, ExpandRowType expandRowType = ExpandRowType.Sequence)
        {
            var (rows, columnPredicate) = CreatePositionRows(puzzle);
            foreach (var rule in puzzle.Rules)
            {
                (rows, columnPredicate) = RuleDlxMapper.GetDlx(rule).ExpandRows(puzzle, rows, columnPredicate, expandRowType);
            }

            return new Dlx(rows.RowsToMatrix(), columnPredicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="puzzle"></param>
        /// <returns>(按position升序的IEnumerable, columnPredicate)</returns>
        private static (IEnumerable<int[]>, int[]) CreatePositionRows(IPuzzle puzzle)
        {
            var rows = Enumerable.Range(0, puzzle.Digits.Length).Select(i =>
            {
                var row = new int[puzzle.Digits.Length];
                row[i] = 1;
                return row;
            });
            // 0:主列 1:副列 2:提示列
            var columnPredicate = new int[puzzle.Digits.Length];

            // 添加 position 提示列
            rows = rows.Select(row => row.Append(GetPosition(row, puzzle)).ToArray());
            columnPredicate = columnPredicate.Append(ColumnPredicateEx.KeyPosition).ToArray();

            // 添加 possible 提示列
            var width = puzzle.Size[0];
            if (width != puzzle.Size[1])
            {
                throw new ArgumentException("暂时仅支持正方形");
            }

            rows = rows.Select(row => row.Append(puzzle.GeneratePossibleDigits()).ToArray());
            columnPredicate = columnPredicate.Append(ColumnPredicateEx.KeyPossibleColumn).ToArray();

            // todo 后续先对 possible 进行一次过滤

            return (rows, columnPredicate);
        }

        private static int GetPosition(int[] row, IPuzzle puzzle)
        {
            var index = Array.IndexOf(row, 1);
            if (index < 0 || index >= puzzle.Digits.Length) throw new ArgumentOutOfRangeException(nameof(index), "Position is out of range.");
            return index;
        }

        public static int GetPosition(int[] row, int[] columnPredicate)
        {
            var index = Array.IndexOf(columnPredicate, ColumnPredicateEx.KeyPosition);
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "Position is not found.");
            return row[index];
        }

        #endregion

        #region ToSolution

        public static int[] ToSolution(IPuzzle puzzle, Dlx dlx, int[] rowIndexes)
        {
            var columnPredicate = dlx.ColumnPredicate;
            var rows = dlx.ReadonlyMatrix.MatrixToRows().ToList();

            var solution = (int[])puzzle.Digits.Clone();
            var resultRows = rowIndexes.Select(rowIndex => rows[rowIndex]).ToList();
            foreach (var rule in puzzle.Rules)
            {
                if (RuleDlxMapper.GetDlx(rule).FillSolution(resultRows, solution, puzzle)) break;
            }

            return solution;
        }

        #endregion
    }
}
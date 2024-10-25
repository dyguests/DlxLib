using System;
using System.Collections.Generic;
using SudokuLib;

namespace SudokuDlxLib.Rules
{
    public interface IRuleDlx
    {
        /// <summary>
        /// 基于 dlx 的 rows，扩展IRule规则
        /// 基于 dlx 的 rows，扩展IRule规则
        /// </summary>
        /// <param name="rows">dlx rows</param>
        /// <param name="columnPredicate">列规则：主/副/提示列</param>
        /// <param name="puzzle"></param>
        /// <returns>(rows, columnPredicate)</returns>
        (IEnumerable<int[]>, int[]) ExpandRows(IEnumerable<int[]> rows, int[] columnPredicate, IPuzzle puzzle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="solution">要填充的solution</param>
        /// <param name="rows">rows 精确匹配（dlx） 后的结果就是 rowIndexes</param>
        /// <param name="puzzle"></param>
        /// <returns>是否填充完毕</returns>
        bool FillSolution(int[] solution, List<int[]> rows, IPuzzle puzzle);
    }

    public abstract class RuleDlx : IRuleDlx
    {
        #region IRule

        public abstract (IEnumerable<int[]>, int[]) ExpandRows(IEnumerable<int[]> rows, int[] columnPredicate, IPuzzle puzzle);

        public abstract bool FillSolution(int[] solution, List<int[]> rows, IPuzzle puzzle);

        #endregion

        #region Rule

        protected static int GetPosition(int[] row, IPuzzle puzzle)
        {
            var index = Array.IndexOf(row, 1);
            if (index < 0 || index >= puzzle.Digits.Length) throw new ArgumentOutOfRangeException(nameof(index), "Position is out of range.");
            return index;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnPredicate"></param>
        /// <returns>用来记录possibleDigits的辅助列的索引</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected int GetPossibleDigitsIndex(int[] columnPredicate)
        {
            var index = Array.IndexOf(columnPredicate, ColumnPredicateEx.KeyPossibleColumn);
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), "Possible column is not found.");
            return index;
        }

        /// <summary>
        /// 更新 可能数字提示列，并返回可能的数字
        /// </summary>
        /// <param name="row"></param>
        /// <param name="columnPredicate">列规则</param>
        /// <param name="puzzle"></param>
        /// <returns>possibleDigits</returns>
        protected IEnumerable<int> UpdatePossibleDigits(int[] row, int[] columnPredicate, IPuzzle puzzle)
        {
            var position = GetPosition(row, puzzle);
            var possibleColumn = GetPossibleDigitsIndex(columnPredicate);
            var digit = puzzle.Digits[position];
            if (digit > 0)
            {
                row[possibleColumn] &= 0b1 << (digit - 1);
            }
            else
            {
                // todo 这里想办法加过滤算法
                row[possibleColumn] &= 0b111_111_111;
            }

            for (var i = 0; i < 9; i++)
            {
                if ((row[possibleColumn] & (0b1 << i)) == 0) continue;
                yield return i + 1;
            }
        }

        #endregion
    }
}
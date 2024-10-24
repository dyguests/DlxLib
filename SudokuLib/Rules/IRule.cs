using System;
using System.Collections.Generic;

namespace SudokuLib.Rules
{
    public interface IRule
    {
        /// <summary>
        /// 基于 dlx 的 rows，扩展IRule规则
        /// 基于 dlx 的 rows，扩展IRule规则
        /// </summary>
        /// <param name="rows">dlx rows</param>
        /// <param name="columnPredicate">列说明：主/副/提示列</param>
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

    public abstract class Rule : IRule
    {
        #region IRule

        public abstract (IEnumerable<int[]>, int[]) ExpandRows(IEnumerable<int[]> rows, int[] columnPredicate, IPuzzle puzzle);

        public abstract bool FillSolution(int[] solution, List<int[]> rows, IPuzzle puzzle);

        #endregion

        #region Rule

        protected static int GetPosition(int[] row, IPuzzle puzzle)
        {
            var position = Array.IndexOf(row, 1);
            if (position < 0 || position >= puzzle.Digits.Length) throw new ArgumentOutOfRangeException(nameof(position), "Position is out of range.");
            return position;
        }

        #endregion
    }
}
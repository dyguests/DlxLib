using System;
using System.Collections.Generic;

namespace SudokuLib.Rules
{
    public interface IRule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rows">dlx rows</param>
        /// <param name="puzzle"></param>
        /// <returns></returns>
        IEnumerable<int[]> ExpandRows(IEnumerable<int[]> rows, IPuzzle puzzle);
    }

    public abstract class Rule : IRule
    {
        #region IRule

        public abstract IEnumerable<int[]> ExpandRows(IEnumerable<int[]> rows, IPuzzle puzzle);

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
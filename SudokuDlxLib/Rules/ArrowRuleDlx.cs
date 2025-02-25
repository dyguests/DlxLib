using System;
using System.Collections.Generic;
using SudokuLib;

namespace SudokuDlxLib.Rules
{
    public class ArrowRuleDlx : RuleDlx
    {
        #region RuleDlx

        public override (IEnumerable<int[]>, int[]) ExpandRows(IPuzzle puzzle, IEnumerable<int[]> rows, int[] columnPredicate, ExpandRowType expandRowType = ExpandRowType.Sequence)
        {
            throw new NotImplementedException();
        }

        public override bool FillSolution(int[] columnPredicate, List<int[]> rows, int[] solution, IPuzzle puzzle)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
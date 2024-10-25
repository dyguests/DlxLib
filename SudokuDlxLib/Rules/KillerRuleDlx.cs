using System;
using System.Collections.Generic;
using System.Linq;
using DlxLib.ColumnPredicates;
using SudokuLib;
using SudokuLib.Rules;

namespace SudokuDlxLib.Rules
{
    internal class KillerRuleDlx : RuleDlx
    {
        #region RuleDlx

        public override (IEnumerable<int[]>, int[]) ExpandRows(IEnumerable<int[]> rows, int[] columnPredicate, IPuzzle puzzle)
        {
            var rule = puzzle.Rules.OfType<KillerRule>().FirstOrDefault() ?? throw new Exception("KillerRule not found");

            var expandRows = rows.SelectMany((row, index) =>
            {
                // if (index == 0) _ruleRowStart = row.Length;

                //todo impl
                return new[] { 1 }.Select(i => { return row; });
            });
            //todo impl
            var expandColumnPredicate = columnPredicate.Concat(Enumerable.Repeat(IndexColumnsPredicate.KeySecondaryColumn, 9 * 2)).ToArray();
            return (expandRows, expandColumnPredicate);
        }

        public override bool FillSolution(int[] solution, List<int[]> rows, IPuzzle puzzle)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
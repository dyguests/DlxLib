using System;
using System.Collections.Generic;
using System.Linq;
using DlxLib.ColumnPredicates;
using SudokuLib;

namespace SudokuDlxLib.Rules
{
    public class DiagonalRuleDlx : RuleDlx
    {
        #region RuleDlx

        // private int _ruleRowStart;

        public override (IEnumerable<int[]>, int[]) ExpandRows(IPuzzle puzzle, IEnumerable<int[]> rows, int[] columnPredicate, ExpandRowType expandRowType = ExpandRowType.Sequence)
        {
            var expandRows = rows.SelectMany((row, index) =>
            {
                // if (index == 0) _ruleRowStart = row.Length;

                row = (int[])row.Clone();

                var position = SudokuDlxUtil.GetPosition(row, puzzle);
                var possibleColumn = GetPossibleDigitsIndex(columnPredicate);
                var possibleDigits = UpdatePossibleDigits(puzzle, row, columnPredicate, expandRowType);

                return possibleDigits.Select(possibleDigit =>
                {
                    var expandingRow = new int[9 * 2];

                    if (position % 9 == position / 9)
                    {
                        expandingRow[possibleDigit - 1] = 1;
                    }

                    if (position % 9 + position / 9 + 1 == 9)
                    {
                        expandingRow[9 + possibleDigit - 1] = 1;
                    }

                    var expandRow = row.Concat(expandingRow).ToArray();
                    expandRow[possibleColumn] = 0b1 << (possibleDigit - 1);
                    return expandRow;
                });
            });
            var expandColumnPredicate = columnPredicate.Concat(Enumerable.Repeat(ColumnPredicate.KeyPrimaryColumn, 9 * 2)).ToArray();
            return (expandRows, expandColumnPredicate);
        }

        public override bool FillSolution(int[] solution, List<int[]> rows, IPuzzle puzzle)
        {
            // todo 后期可能有用，比如如果没有标准数独规则时。
            throw new NotImplementedException();
        }

        #endregion
    }
}
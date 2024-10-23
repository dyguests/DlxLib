using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuLib.Rules
{
    public class DiagonalRule : Rule
    {
        #region DiagonalRule

        // private int _ruleRowStart;

        public override IEnumerable<int[]> ExpandRows(IEnumerable<int[]> rows, IPuzzle puzzle)
        {
            return rows.SelectMany((row, index) =>
            {
                // if (index == 0) _ruleRowStart = row.Length;

                var position = GetPosition(row, puzzle);
                var digit = puzzle.Digits[position];
                var possibleDigits = digit == 0
                    ? Enumerable.Range(1, 9).ToArray()
                    : new[]
                    {
                        digit
                    };

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

                    return row.Concat(expandingRow).ToArray();
                });
            });
        }

        public override bool FillSolution(int[] solution, List<int[]> rows, IPuzzle puzzle)
        {
            // todo 后期可能有用，比如如果没有标准数独规则时。
            throw new NotImplementedException();
        }

        #endregion
    }
}
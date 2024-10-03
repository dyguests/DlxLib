using System.Collections.Generic;
using System.Linq;

namespace SudokuLib.Rules
{
    public class StandardRule : BaseRule
    {
        #region Rule

        public override IEnumerable<int[]> ExpandRows(IEnumerable<int[]> rows, IPuzzle puzzle)
        {
            return rows.SelectMany(row =>
            {
                var position = GetPosition(row, puzzle);
                var digit = puzzle.Digits[position];
                var possibleDigits = digit == 0 ? Enumerable.Range(1, 9).ToArray() : new[] { digit };
                return possibleDigits.Select(possibleDigit =>
                {
                    var standardRow = new int[ /*rowCount*digitCount*/9 * 9 + /*colCount*digitCount*/9 * 9 + /*boxCount*digitCount*/9 * 9];
                    standardRow[ /*rowIndex*digitCount*/position / 9 * 9 + possibleDigit - 1] = 1;
                    standardRow[ /*rowCount*digitCount*/9 * 9 + /*columnIndex*digitCount*/position % 9 * 9 + possibleDigit - 1] = 1;
                    standardRow[ /*rowCount*digitCount*/
                        9 * 9 + /*colCount*digitCount*/9 * 9 + /*boxIndex*digitCount*/(position / 9 / 3 * 3 + position % 9 / 3) * 9 + possibleDigit - 1] = 1;
                    return row.Concat(standardRow).ToArray();
                });
            });
        }

        #endregion
    }
}
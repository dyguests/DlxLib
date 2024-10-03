using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuLib.Rules
{
    public class StandardRule : BaseRule
    {
        #region Rule

        public override IEnumerable<int[]> ExpandRows(IEnumerable<int[]> rows, IPuzzle puzzle)
        {
            var ruleRowStart = 0;
            const int ruleRowCount = 9 * 9 + /*colCount*digitCount*/9 * 9 + /*boxCount*digitCount*/9 * 9;

            var expandedRows = rows.SelectMany((row, index) =>
            {
                if (index == 0) ruleRowStart = row.Length;

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

            RuleRowRange = new Range(ruleRowStart, ruleRowStart + ruleRowCount);

            return expandedRows;
        }

        #endregion

        #region StandardRule

        /// <summary>
        /// 当前rule负责的row中的range
        /// </summary>
        private Range RuleRowRange { get; set; }

        #endregion
    }
}
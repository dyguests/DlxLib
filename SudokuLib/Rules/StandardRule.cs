using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuLib.Rules
{
    /// <summary>
    /// 标准数独规则
    /// </summary>
    public class StandardRule : BaseRule
    {
        #region Rule

        private int _ruleRowStart;

        public override IEnumerable<int[]> ExpandRows(IEnumerable<int[]> rows, IPuzzle puzzle)
        {
            return rows.SelectMany((row, index) =>
            {
                if (index == 0) _ruleRowStart = row.Length;

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
                    var standardRow = new int[ /*rowCount*digitCount*/ 9 * 9 + /*colCount*digitCount*/9 * 9 + /*boxCount*digitCount*/9 * 9];
                    standardRow[ /*rowIndex*digitCount*/position / 9 * 9 + possibleDigit - 1] = 1;
                    standardRow[ /*rowCount*digitCount*/9 * 9 + /*columnIndex*digitCount*/position % 9 * 9 + possibleDigit - 1] = 1;
                    standardRow[ /*rowCount*digitCount*/
                        9 * 9 + /*colCount*digitCount*/9 * 9 + /*boxIndex*digitCount*/(position / 9 / 3 * 3 + position % 9 / 3) * 9 + possibleDigit - 1] = 1;
                    return row.Concat(standardRow).ToArray();
                });
            });
        }

        public override bool FillSolution(int[] solution, List<int[]> rows, IPuzzle puzzle)
        {
            var isAllFilled = true;
            for (var position = 0; position < solution.Length; position++)
            {
                var digit = solution[position];
                if (digit != 0) continue;
                var row = rows.FirstOrDefault(row => row[position] == 1) ?? throw new NullReferenceException("rows 未包含 row[position] == 1");
                var startIndex = _ruleRowStart;
                var endIndex = startIndex + /*rowCount*digitCount*/ 9 * 9;
                var index = Array.FindIndex(row, startIndex, endIndex - startIndex, value => value == 1);
                if (index < startIndex || index >= endIndex)
                {
                    isAllFilled = false;
                    continue;
                }

                var indexInRule = index - startIndex;
                solution[position] = indexInRule % 9 + 1;
            }

            return isAllFilled;
        }

        #endregion
    }
}
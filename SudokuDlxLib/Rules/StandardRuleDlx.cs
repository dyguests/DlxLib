using System;
using System.Collections.Generic;
using System.Linq;
using SudokuDlxLib.Utils;
using SudokuLib;

namespace SudokuDlxLib.Rules
{
    /// <summary>
    /// 标准数独规则
    /// </summary>
    public class StandardRuleDlx : BaseRuleDlx
    {
        #region Rule

        private int _ruleRowStart;

        public override (IEnumerable<int[]>, int[]) ExpandRows(IPuzzle puzzle, IEnumerable<int[]> rows, int[] columnPredicate, ExpandRowType expandRowType = ExpandRowType.Sequence)
        {
            var size = puzzle.Size;
            var candidateCount = puzzle.GetCandidateCount();
            var hasBox = size[0] is 4 or 6 or 8 or 9; // 是否有宫（Box）// todo 暂时宽高一致；后续研究下不同尺寸的处理

            var rowsXCc = size[1] * candidateCount; // 总行数*可选数字数
            var columnsCc = size[0] * candidateCount; // 总列数*可选数字数
            var tileCount = hasBox ? Math.Max(size[0], size[1]) : 0; // 一个宫中有几个格子
            var boxSize = new int[2];
            switch (size[0])
            {
                case 4:
                    boxSize[0] = 2;
                    boxSize[1] = 2;
                    break;
                case 6:
                    boxSize[0] = 3;
                    boxSize[1] = 2;
                    break;
                case 8:
                    boxSize[0] = 4;
                    boxSize[1] = 2;
                    break;
                case 9:
                    boxSize[0] = 3;
                    boxSize[1] = 3;
                    break;
            }

            var boxesXCc = tileCount * candidateCount; // 总宫数*可选数字数

            var expandRows = rows.SelectMany((row, index) =>
            {
                if (index == 0) _ruleRowStart = row.Length;

                row = (int[])row.Clone();

                var position = SudokuDlxUtil.GetPosition(row, columnPredicate);
                var possibleDigitsIndex = GetPossibleDigitsIndex(columnPredicate);
                var possibleDigits = UpdatePossibleDigits(puzzle, row, columnPredicate, expandRowType);

                return possibleDigits.Select(possibleDigit =>
                {
                    var currRowXCc = position / size[0] * candidateCount; // 当前行*可选数字数
                    var currColumnXCc = position % size[0] * candidateCount; // 当前列*可选数字数
                    var expandingRow = new int[rowsXCc + columnsCc];
                    expandingRow[currRowXCc + (possibleDigit - 1)] = 1;
                    expandingRow[rowsXCc + currColumnXCc + (possibleDigit - 1)] = 1;
                    if (hasBox)
                    {
                        var currBoxXCc = (position / size[0] / boxSize[1] * boxSize[1] + position % size[0] / boxSize[0]) * candidateCount; // 当前宫*可选数字数
                        expandingRow = expandingRow.Concat(new int[boxesXCc]).ToArray();
                        expandingRow[rowsXCc + columnsCc + currBoxXCc + (possibleDigit - 1)] = 1;
                    }

                    var expandRow = row.Concat(expandingRow).ToArray();
                    expandRow[possibleDigitsIndex] = 0b1 << (possibleDigit - 1);
                    return expandRow;
                });
            });
            var expandColumnPredicate = columnPredicate.Concat(new int[rowsXCc + columnsCc + boxesXCc]).ToArray();
            return (expandRows, expandColumnPredicate);
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
                var endIndex = startIndex + puzzle.Digits.Length;
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
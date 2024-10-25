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
            var possibleDigitsIndex = GetPossibleDigitsIndex(columnPredicate);

            var rule = puzzle.Rules.OfType<KillerRule>().FirstOrDefault() ?? throw new Exception("KillerRule not found");
            var cages = rule.ReadonlyCages;
            var allPositions = cages.SelectMany(cage => cage.Indexes).Order();

            // gen position -> possibleDigits
            var pos2possibleDigits = rows.Select(row => (position: GetPosition(row, puzzle), row))
                .Where(tuple => allPositions.Contains(tuple.position))
                .GroupBy<(int position, int[] row), int>(tuple => tuple.position)
                // .GroupBy(row => GetPosition(row, puzzle))
                .ToDictionary<IGrouping<int, (int position, int[] row)>, int, int[]>(
                    group => group.Key,
                    group => group.AsEnumerable()
                        .Select(tuple => tuple.row)
                        .Select(row => row[possibleDigitsIndex])
                        .Aggregate((a, b) => a | b)
                        .PossibleDigitsFromBinaryToEnumerable()
                        .ToArray()
                );


            var expandRows = rows.SelectMany((row, index) =>
            {
                // if (index == 0) _ruleRowStart = row.Length;

                //todo impl
                return new[] { 1 }.Select(i => { return row; });
            });
            //todo impl
            var expandColumnPredicate = columnPredicate.Concat(Enumerable.Repeat(ColumnPredicate.KeySecondaryColumn, 9 * 2)).ToArray();
            return (expandRows, expandColumnPredicate);
        }

        public override bool FillSolution(int[] solution, List<int[]> rows, IPuzzle puzzle)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using DlxLib.ColumnPredicates;
using SudokuLib;
using SudokuLib.Helpers;
using SudokuLib.Rules;

namespace SudokuDlxLib.Rules
{
    public class KillerRuleDlx : RuleDlx
    {
        #region RuleDlx

        public override (IEnumerable<int[]>, int[]) ExpandRows(IPuzzle puzzle, IEnumerable<int[]> rows, int[] columnPredicate, ExpandRowType expandRowType = ExpandRowType.Sequence)
        {
            var possibleDigitsIndex = GetPossibleDigitsIndex(columnPredicate);

            var rule = puzzle.Rules.OfType<KillerRule>().FirstOrDefault() ?? throw new Exception("KillerRule not found");
            var cages = rule.ReadonlyCages;
            var allPositions = cages.SelectMany(cage => cage.Indexes).OrderBy(i => i);

            var rowArray = rows.ToArray();

            // gen position -> possibleDigits
            var pos2PossibleDigits = rowArray.Select(row => (position: GetPosition(row, puzzle), row))
                .Where(tuple => allPositions.Contains(tuple.position))
                .GroupBy(tuple => tuple.position)
                .ToDictionary(
                    group => group.Key,
                    group => group.AsEnumerable()
                        .Select(tuple => tuple.row)
                        .Select(row => row[possibleDigitsIndex])
                        .Aggregate((a, b) => a | b)
                        .PossibleDigitsFromBinaryToEnumerable(expandRowType != ExpandRowType.Sequence)
                        .ToArray()
                );

            var cageCombinationPermutations = cages.Select(cage => (cage, combinations: KillerRuleHelper.GetPossibleCombinations(cage)))
                    .SelectMany(tuple => tuple.combinations.Select(combination => (tuple.cage, combination)))
                    .SelectMany(tuple =>
                        GetPossiblePermutations(
                                tuple.combination,
                                tuple.cage.Indexes.Select(position => pos2PossibleDigits[position]).ToArray()
                            )
                            .Select(permutation => (tuple.cage, tuple.combination, permutation))
                    )
                ;

            var expandRows = rowArray.SelectMany((row, index) =>
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

        /// <summary>
        /// 返回组合可能的排列
        ///
        /// 例：组合{1,2,3}，每个位置上可能的取值为{{2,3},{2},{1,2,3}}
        /// 即第一个位置只能取1或2，第二个位置只能取2，第三个位置只能取1或2或3
        /// 最终返回的排列也只能是 {3,2,1}
        /// 
        /// </summary>
        /// <param name="combination">组合</param>
        /// <param name="indexesIncludePossibleDigits">排列的每个位置上可能的取值</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private IEnumerable<int[]> GetPossiblePermutations(int[] combination, int[][] indexesIncludePossibleDigits)
        {
            if (combination.Length != indexesIncludePossibleDigits.Length) throw new Exception("combination.Length != indexes.Length");

            return Permute((int[])combination.Clone(), 0);

            // 递归生成排列，使用 IEnumerable<int[]>
            IEnumerable<int[]> Permute(int[] combination, int start)
            {
                if (start >= combination.Length)
                {
                    // 当排列完成时，yield return 当前排列的副本
                    yield return (int[])combination.Clone();
                }
                else
                {
                    for (var i = start; i < combination.Length; i++)
                    {
                        // includePossibleDigits 过滤
                        if (!indexesIncludePossibleDigits[start].Contains(combination[i])) continue;

                        // 交换元素
                        Swap(ref combination[start], ref combination[i]);
                        // 递归生成排列
                        foreach (var perm in Permute(combination, start + 1))
                        {
                            yield return perm;
                        }

                        // 回溯交换回原位
                        Swap(ref combination[start], ref combination[i]);
                    }
                }
            }

            // 交换数组中的两个元素
            static void Swap(ref int a, ref int b) => (a, b) = (b, a);
        }

        [Obsolete]
        public static IEnumerable<int[]> TestGetPossiblePermutations(int[] combination, int[][] indexesIncludePossibleDigits)
        {
            return new KillerRuleDlx().GetPossiblePermutations(combination, indexesIncludePossibleDigits);
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using DlxLib.ColumnPredicates;
using SudokuDlxLib.Utils;
using SudokuLib;
using SudokuLib.Helpers;
using SudokuLib.Rules;

namespace SudokuDlxLib.Rules
{
    public class KillerRuleDlx : RuleDlx
    {
        #region RuleDlx

        /// <summary>
        /// ExpandRows 之前的 row.Length，也是新的 rule 的 row 开始位置
        /// </summary>
        private int _ruleRowStart;

        public override (IEnumerable<int[]>, int[]) ExpandRows(
            IPuzzle puzzle,
            IEnumerable<int[]> rows,
            int[] columnPredicate,
            ExpandRowType expandRowType = ExpandRowType.Sequence
        )
        {
            // 取得 possibleDigits 在 row 中的 index
            var possibleDigitsIndex = GetPossibleDigitsIndex(columnPredicate);

            var rule = puzzle.Rules.OfType<KillerRule>().FirstOrDefault() ?? throw new Exception("KillerRule not found");
            var cages = rule.ReadonlyCages;

            // position to cage在Cages中的索引 的对应关系
            var position2CageIndex = cages.SelectMany((cage, cageIndex) => cage.Indexes.Select(position => (position, cageIndex)))
                .ToDictionary(tuple => tuple.position, tuple => tuple.cageIndex);

            // 所有cage中的所有位置
            var positionsInCages = cages.SelectMany(cage => cage.Indexes).OrderBy(i => i);
            // 每个cage中的第一个位置的集合
            var firstPositionsInCages = cages.Select(cage => cage.Indexes.First()).OrderBy(i => i).ToArray();

            var rowArray = rows.ToArray();

            // poisiton to possibleDigits 的对应关系，这个还未基于cage过滤
            var position2PossibleDigits = rowArray.Select(row => (position: SudokuDlxUtil.GetPosition(row, columnPredicate), row))
                .Where(tuple => positionsInCages.Contains(tuple.position))
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

            // cage to combinations 的对应关系
            var cage2Combinations = cages.ToDictionary(
                cage => cage,
                cage =>
                {
                    var possibleDigits = cage.Indexes.SelectMany(position => position2PossibleDigits[position]).Distinct().OrderBy(i => i).ToArray();
                    return KillerRuleHelper.GetPossibleCombinations(cage, possibleDigits).ToArray();
                }
            );

            var cage2PossibleDigits = cage2Combinations.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.Select(combination => combination.GetPossibleDigits())
                    .SelectMany(combination => combination).Distinct()
                    .OrderBy(digit => digit)
                    .ToArray()
            );

            var expandRows = rowArray.SelectMany((row, index) =>
            {
                if (index == 0) _ruleRowStart = row.Length;

                var position = SudokuDlxUtil.GetPosition(row, columnPredicate);

                IEnumerable<int[]> GenerateExpandRow(int digit)
                {
                    // todo 这里宽度不一定要用9，后续再优化

                    // 若当前row的position不在任何一个cage中，则直接返回一个空约束
                    if (!position2CageIndex.TryGetValue(position, out var cageIndex))
                    {
                        yield return new int[9];
                        yield break;
                    }

                    var hasPossibleDigits = cage2PossibleDigits.TryGetValue(cages[cageIndex], out var possibleDigits);
                    if (!hasPossibleDigits) throw new Exception("possibleDigitsInCage is null，不应到达的分支");
                    if (possibleDigits == null) throw new Exception("possibleDigits is null，不应到达的分支");

                    // 若当前digit不在所在的cage的可能排列中，则直接返回0个约束
                    if (!possibleDigits.Contains(digit))
                    {
                        yield break;
                    }

                    // 若当前position是cage中的第一个位置，且cage.sum>0，则需要将除cage中其它位置全标1
                    // 因为sum>0 要用 DLX的完全覆盖，sum=0 时只不需要完全覆盖（只需要不冲突）
                    if (firstPositionsInCages.Contains(position) && cages[cageIndex].Sum > 0)
                    {
                        cage2Combinations.TryGetValue(cages[cageIndex], out var combinations);
                        if (combinations == null) throw new Exception("combinations is null，不应到达的分支");
                        foreach (var combination in combinations)
                        {
                            var combinationPossibleDigits = combination.GetPossibleDigits();
                            if (Array.IndexOf(combinationPossibleDigits, digit) < 0) continue;
                            var expandingRow = Enumerable.Repeat(1, 9).ToArray();
                            foreach (var aDigit in combinationPossibleDigits)
                            {
                                expandingRow[aDigit - 1] = 0;
                            }

                            expandingRow[digit - 1] = 1;
                            yield return expandingRow;
                        }

                        yield break;
                    }

                    // 对于cage中的非第一个位置，或sum=0，只需要标1就行
                    {
                        var expandingRow = new int[9];
                        expandingRow[digit - 1] = 1;
                        yield return expandingRow;
                    }
                }

                return row[possibleDigitsIndex].PossibleDigitsFromBinaryToEnumerable()
                    .SelectMany(GenerateExpandRow)
                    .Select(expandingRow =>
                    {
                        var array = new int[9 * cages.Length];
                        if (position2CageIndex.TryGetValue(position, out var cageIndex))
                        {
                            Array.Copy(expandingRow, 0, array, 9 * cageIndex, expandingRow.Length);
                        }

                        return array;
                    })
                    .Distinct(new IntArrayComparer())
                    .Select(expandingRow =>
                    {
                        var array = row.Concat(expandingRow).ToArray();
                        // Console.WriteLine($"expandingRow:{string.Join(",", array.Skip(_ruleRowStart))}");
                        return array;
                    });
            });
            var expandColumnPredicate = columnPredicate.Concat(
                // Enumerable.Repeat(ColumnPredicate.KeyPrimaryColumn, 9 * cages.Length)
                cages.Select(cage =>
                {
                    // sum>0 要用 DLX的完全覆盖，sum=0 时只不需要完全覆盖（只需要不冲突）
                    if (cage.Sum > 0)
                    {
                        return Enumerable.Repeat(ColumnPredicate.KeyPrimaryColumn, 9);
                    }
                    else
                    {
                        return Enumerable.Repeat(ColumnPredicate.KeySecondaryColumn, 9);
                    }
                }).SelectMany(ints => ints)
            ).ToArray();
            return (expandRows, expandColumnPredicate);
        }

        public override bool FillSolution(int[] columnPredicate, List<int[]> rows, int[] solution, IPuzzle puzzle)
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
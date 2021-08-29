using System;
using System.Collections.Generic;
using System.Linq;
using SudokuDlxLib.Utils;
using SudokuLib;

namespace SudokuDlxLib.Processors
{
    public class CageRuleDlxProcessor : RuleDlxProcessor
    {
        public override void ReducePossibleNumbers(Sudoku sudoku, int[][] possibleNumbersIndexes)
        {
            sudoku.GetRule<CageRule>().cages.ForEach(cage => ReduceCagePossibleNumbers(possibleNumbersIndexes, cage));
        }

        public override RuleMatrix RuleToMatrix(Sudoku sudoku, int[][] possibleNumbersIndexes)
        {
            (int[,] matrix, int[] primaryColumns, int[] secondaryColumns) = ToMatrix(sudoku.GetRule<CageRule>(), possibleNumbersIndexes);
            return new RuleMatrix
            {
                type = RuleType.Cage,
                matrix = matrix,
                primaryColumns = primaryColumns,
                secondaryColumns = secondaryColumns,
            };
        }

        private void ReduceCagePossibleNumbers(int[][] possibleNumbersIndexes, CageRule.Cage cage)
        {
            // key:numberIndex, value:possibleNumbers
            var cagePossibleNumbersIndexes = new Dictionary<int, HashSet<int>>();
            FindCagePossibleNumbers(possibleNumbersIndexes, cage, cagePossibleNumbersIndexes, 0, new Dictionary<int, int>());

            foreach (var pair in cagePossibleNumbersIndexes)
            {
                possibleNumbersIndexes[pair.Key] = possibleNumbersIndexes[pair.Key].Intersect(pair.Value).ToArray();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="possibleNumbersIndexes"></param>
        /// <param name="cage"></param>
        /// <param name="cagePossibleNumbersIndexes"></param>
        /// <param name="cageIndex"></param>
        /// <param name="currentNumbers"> todo change to stack
        /// </param>
        private void FindCagePossibleNumbers(int[][] possibleNumbersIndexes, CageRule.Cage cage, Dictionary<int, HashSet<int>> cagePossibleNumbersIndexes, int cageIndex, Dictionary<int, int> currentNumbers)
        {
            if (cageIndex >= cage.indexes.Length)
            {
                // 如果 笼子 有求和且不匹配，则不满足条件
                if (cage.sum > 0 && cage.sum != currentNumbers.Sum(pair => pair.Value))
                {
                    return;
                }

                for (var _cageIndex = 0; _cageIndex < cage.indexes.Length; _cageIndex++)
                {
                    var numberIndex = cage.indexes[_cageIndex];

                    if (!cagePossibleNumbersIndexes.TryGetValue(numberIndex, out var cagePossibleNumbers))
                    {
                        cagePossibleNumbers = new HashSet<int>();
                        cagePossibleNumbersIndexes[numberIndex] = cagePossibleNumbers;
                    }

                    cagePossibleNumbers.Add(currentNumbers[_cageIndex]);
                }

                return;
            }

            {
                var numberIndex = cage.indexes[cageIndex];
                var possibleNumbers = possibleNumbersIndexes[numberIndex];
                foreach (var possibleNumber in possibleNumbers)
                {
                    if (currentNumbers.Any(pair => pair.Value == possibleNumber)) continue;

                    currentNumbers[cageIndex] = possibleNumber;
                    FindCagePossibleNumbers(possibleNumbersIndexes, cage, cagePossibleNumbersIndexes, cageIndex + 1, currentNumbers);
                }
            }
        }

        private (int[,] matrix, int[] primaryColumns, int[] secondaryColumns) ToMatrix(CageRule rule, int[][] possibleNumbersIndexes)
        {
            var cageMatrixss = rule.cages.Select(cage =>
            {
                if (cage.sum > 0)
                {
                    return GetSumMatrix(possibleNumbersIndexes, cage);
                }
                else
                {
                    return GetUniqueMatrix(cage, possibleNumbersIndexes);
                }
            });
            cageMatrixss.ForEach(cageMatrixs => cageMatrixs.ForEach(combination => Console.WriteLine("cageMatrix:" + string.Join(",", combination))));
            return (null, null, null);
        }

        private IEnumerable<int[]> GetSumMatrix(int[][] possibleNumbersIndexes, CageRule.Cage cage)
        {
            // 返回可能的组合。例如：2,1,6; 1,2,6; 2,3,4 
            var possibleCombinations = GetPossibleCombinations(cage, possibleNumbersIndexes, 0, new int[0]);
            // 去掉重复(忽略顺序，仅保留升序)的组合。例如：2,1,6; 1,2,6; 2,3,4 -> 1,2,6; 2,3,4
            var combinations = possibleCombinations.Select(ints => new HashSet<int>(ints)).Distinct().Select(set => set.ToArray()).Select(ints => ints.Also(Array.Sort)).ToArray();
            
            return possibleCombinations;
        }

        /// <summary>
        /// 存在数字和
        /// 
        /// 杀手数独->matrix
        /// </summary>
        /// <param name="cage"></param>
        /// <param name="possibleNumbersIndexes"></param>
        /// <param name="cageIndex"></param>
        /// <param name="combination"></param>
        /// <returns></returns>
        private IEnumerable<int[]> GetPossibleCombinations(CageRule.Cage cage, int[][] possibleNumbersIndexes, int cageIndex, int[] combination)
        {
            var numberIndex = cage.indexes[cageIndex];
            var possibleNumbers = possibleNumbersIndexes[numberIndex];
            foreach (var possibleNumber in possibleNumbers)
            {
                if (combination.Contains(possibleNumber)) continue;

                combination = combination.Add(possibleNumber);
                if (cageIndex == cage.indexes.Length - 1)
                {
                    if (combination.Sum() == cage.sum)
                    {
                        yield return combination;
                    }
                }
                else
                {
                    foreach (var numberCombination in GetPossibleCombinations(cage, possibleNumbersIndexes, cageIndex + 1, combination)) yield return numberCombination;
                }
            }
        }

        /// <summary>
        /// 不存在数字和
        ///
        /// 副列->matrix
        /// </summary>
        /// <param name="cage"></param>
        /// <param name="possibleNumbersIndexes"></param>
        /// <returns></returns>
        private IEnumerable<int[]> GetUniqueMatrix(CageRule.Cage cage, int[][] possibleNumbersIndexes)
        {
            throw new NotImplementedException();
        }
    }
}
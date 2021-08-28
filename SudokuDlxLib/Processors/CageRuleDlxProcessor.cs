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
            // var cageMatrix = rule.cages.Select(cage => GenerateNumberCombinations(cage, possibleNumbersIndexes));
            return (null, null, null);
        }

        private IEnumerable<int[]> GenerateNumberCombinations(CageRule.Cage cage, int[][] possibleNumbersIndexes)
        {
            if (cage.sum > 0)
            {
                return GenerateSumNumberCombinations(cage, possibleNumbersIndexes);
            }
            else
            {
                return GenerateNoSumNumberCombinations(cage, possibleNumbersIndexes);
            }
        }

        private IEnumerable<int[]> GenerateSumNumberCombinations(CageRule.Cage cage, int[][] possibleNumbersIndexes)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<int[]> GenerateNoSumNumberCombinations(CageRule.Cage cage, int[][] possibleNumbersIndexes)
        {
            throw new NotImplementedException();
        }
    }
}
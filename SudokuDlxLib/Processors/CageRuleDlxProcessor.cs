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
            var matrix = new Matrix();

            foreach (var cage in rule.cages)
            {
                if (cage.sum > 0)
                {
                    matrix.Expand(GetSumMatrix(possibleNumbersIndexes, cage));
                }
                else
                {
                    var uniqueMatrix = GetUniqueMatrix(cage, possibleNumbersIndexes);
                }
            }

            return (matrix.matrix, matrix.primaryColumns, matrix.secondaryColumns);
        }

        private Matrix GetSumMatrix(int[][] possibleNumbersIndexes, CageRule.Cage cage)
        {
            // 返回可能的组合。例如：2,1,6; 1,2,6; 2,3,4 
            var possibleCombinations = GetPossibleCombinations(cage, possibleNumbersIndexes, 0, new Dictionary<int, int>());
            // 去掉重复(忽略顺序，仅保留升序)的组合。例如：2,1,6; 1,2,6; 2,3,4 -> 1,2,6; 2,3,4
            var combinations = possibleCombinations.Select(ints => ints.Also(Array.Sort)).Distinct(new ArrayComparer()).ToArray();
            var rows = GetSumRows(cage, 0, possibleNumbersIndexes, combinations, new Dictionary<int, int>()).ToArray();
            if (rows.Length == 0)
            {
                Console.WriteLine("GetSumMatrix rows is empty");
                rows = possibleNumbersIndexes[cage.indexes[0]]
                    .Select(possibleNumber => new int[TileCount + NumberCount + cage.sum]
                        .Also(ints =>
                        {
                            ints[cage.indexes[0]] = 1;
                            ints[TileCount + possibleNumber - 1] = 1;
                        })
                    )
                    .ToArray();
            }

            var matrix = ArrayUtil.To2DArray(rows);
            var primaryColumns = Enumerable.Range(0, TileCount).ToList();
            primaryColumns.AddRange(Enumerable.Range(TileCount + NumberCount, matrix.GetLength(1) - TileCount + NumberCount));
            return new Matrix
            {
                matrix = matrix,
                primaryColumns = primaryColumns.ToArray(),
                secondaryColumns = new int[0],
            };
        }

        private IEnumerable<int[]> GetSumRows(CageRule.Cage cage, int cageIndex, int[][] possibleNumbersIndexes, int[][] combinations, Dictionary<int, int> currCombination)
        {
            var numberIndex = cage.indexes[cageIndex];
            foreach (var possibleNumber in possibleNumbersIndexes[numberIndex])
            {
                if (currCombination.Values.Contains(possibleNumber)) continue;
                var matchedCombinations = combinations.Where(combination => currCombination.Values.Where(number => number > 0).All(combination.Contains));
                if (!matchedCombinations.Any(combination => combination.Contains(possibleNumber))) continue;
                currCombination[cageIndex] = possibleNumber;
                if (cageIndex < cage.indexes.Length - 1)
                {
                    foreach (var row in GetSumRows(cage, cageIndex + 1, possibleNumbersIndexes, combinations, currCombination))
                    {
                        yield return row;
                    }
                }
                else if (cageIndex == cage.indexes.Length - 1 /*&& currCombination.Sum() == cage.sum*/)
                {
                    // Console.WriteLine("GetSumRows combination:" + string.Join(",", currCombination.Values));
                    // matrix row
                    foreach (var row in GetSumMatrixRows(cage, combinations, currCombination.OrderBy(pair => pair.Key).Select(pair => pair.Value).ToArray()))
                    {
                        yield return row;
                    }
                }

                currCombination.Remove(cageIndex);
            }
        }

        private IEnumerable<int[]> GetSumMatrixRows(CageRule.Cage cage, int[][] combinations, int[] currCombination)
        {
            var orderCombination = combinations.First(_orderCombination => currCombination.All(_orderCombination.Contains));
            for (var cageIndex = 0; cageIndex < cage.indexes.Length; cageIndex++)
            {
                var row = new int[TileCount + NumberCount + cage.sum];
                var numberIndex = cage.indexes[cageIndex];
                row[numberIndex] = 1;
                var currNumber = currCombination[cageIndex];
                row[TileCount + currNumber - 1] = 1;
                var indexOfOrderCombination = Array.IndexOf(orderCombination, currNumber);
                var preCombinationIndex = orderCombination.Where((number, index) => index < indexOfOrderCombination).Sum();
                // fill row
                for (var i = 0; i < currNumber; i++)
                {
                    row[TileCount + NumberCount + preCombinationIndex + i] = 1;
                }

                yield return row;
            }
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
        private IEnumerable<int[]> GetPossibleCombinations(CageRule.Cage cage, int[][] possibleNumbersIndexes, int cageIndex, Dictionary<int, int> combination)
        {
            var numberIndex = cage.indexes[cageIndex];
            var possibleNumbers = possibleNumbersIndexes[numberIndex];
            foreach (var possibleNumber in possibleNumbers)
            {
                if (combination.Values.Contains(possibleNumber)) continue;

                combination[cageIndex] = possibleNumber;
                if (cageIndex == cage.indexes.Length - 1)
                {
                    if (combination.Values.Sum() == cage.sum)
                    {
                        yield return combination.Values.ToArray();
                    }
                }
                else if (cageIndex < cage.indexes.Length - 1)
                {
                    foreach (var numberCombination in GetPossibleCombinations(cage, possibleNumbersIndexes, cageIndex + 1, combination)) yield return numberCombination;
                }

                combination.Remove(cageIndex);
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
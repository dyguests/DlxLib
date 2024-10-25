using System;
using System.Collections.Generic;
using System.Linq;
using SudokuLib.Rules;

namespace SudokuLib.Helpers
{
    public static class KillerRuleHelper
    {
        public static int[] GetPossibleDigits(KillerRule.Cage cage)
        {
            return GetPossibleCombinations(cage).SelectMany(x => x).Distinct().Order().ToArray();
        }

        public static IEnumerable<int[]> GetPossibleCombinations(KillerRule.Cage cage)
        {
            return GetPossibleCombinations(cage, Enumerable.Range(1, 9).ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cage"></param>
        /// <param name="possibleDigits">建议升序</param>
        /// <returns></returns>
        /// <exception cref="Exception">非法</exception>
        public static IEnumerable<int[]> GetPossibleCombinations(KillerRule.Cage cage, int[] possibleDigits)
        {
            var count = cage.Indexes.Length;
            if (count == 0) throw new Exception("Cage has no indexes");

            possibleDigits = possibleDigits.Order().ToArray();
            if (possibleDigits.Length < count) throw new Exception("Possible digits count is less than cage count");

            var sum = cage.Sum;
            if (possibleDigits.Take(count).Sum() > sum) yield break;
            if (possibleDigits.TakeLast(count).Sum() < sum) yield break;

            foreach (var findPossibleCombination in FindPossibleCombinations(new List<int>(), possibleDigits, count, sum))
            {
                yield return findPossibleCombination;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="combination"> indexes of possibleDigits</param>
        /// <param name="possibleDigits"></param>
        /// <param name="remainCount"></param>
        /// <param name="remainSum"></param>
        /// <returns></returns>
        private static IEnumerable<int[]> FindPossibleCombinations(List<int> combination, int[] possibleDigits, int remainCount, int remainSum)
        {
            var lastIndex = -1;
            if (combination.Count > 0)
            {
                lastIndex = combination.Last();
            }

            for (var i = lastIndex + 1; i < possibleDigits.Length; i++)
            {
                var possibleDigit = possibleDigits[i];
                combination.Add(i);
                remainCount--;
                remainSum -= possibleDigit;

                if (remainCount > 0)
                {
                    var subPossibleDigits = possibleDigits;
                    foreach (var findPossibleCombination in FindPossibleCombinations(combination, subPossibleDigits, remainCount, remainSum))
                    {
                        yield return findPossibleCombination;
                    }
                }
                else if (remainCount == 0 && remainSum == 0)
                {
                    yield return combination.Select(index => possibleDigits[index]).ToArray();
                }

                combination.Remove(i);
                // combination.RemoveAt(combination.Count-1);
                remainCount++;
                remainSum += possibleDigit;
            }
        }
    }
}
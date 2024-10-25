using System.Collections.Generic;
using System.Linq;
using SudokuLib.Rules;

namespace SudokuLib.Helpers
{
    public static class KillerRuleHelper
    {
        public static int[] GetPossibleNumbers(KillerRule.Cage cage)
        {
            return GetPossibleCombinations(cage).SelectMany(x => x).Distinct().Order().ToArray();
        }

        public static IEnumerable<int[]> GetPossibleCombinations(KillerRule.Cage cage)
        {
            return null;
        }
    }
}
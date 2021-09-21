using System;
using System.Linq;
using Databases.Converters;
using SudokuDlxLib.Utils;
using SudokuLib;

namespace SudokuConverterLib.Converters
{
    public class CageRuleConverter : IRuleConverter
    {
        private static readonly string RulePrefix = RuleType.Cage.ToString();

        private static CageRuleConverter sInstance;

        private CageRuleConverter()
        {
        }

        public static CageRuleConverter GetInstance()
        {
            return sInstance ?? (sInstance = new CageRuleConverter());
        }

        public string ToDataString(Sudoku sudoku, Rule rule)
        {
            CageRule rule1 = rule as CageRule;
            return RulePrefix + ":" + string.Join(";", rule1.cages.Select(CageToString));

            string CageToString(CageRule.Cage cage) => cage.sum + "," + string.Join(",", cage.indexes);
        }

        public Rule ToRule(string content)
        {
            var cageStrings = content.Split(';');
            return new CageRule
            {
                cages = cageStrings.Select(ToCage).ToArray(),
            };

            CageRule.Cage ToCage(string cageStr)
            {
                var parts = cageStr.Split(',').Select(part => Convert.ToInt32(part)).ToArray();
                return new CageRule.Cage
                {
                    sum = parts[0],
                    indexes = parts.SubArray(1, parts.Length - 1),
                };
            }
        }
    }
}
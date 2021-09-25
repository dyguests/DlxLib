using System.IO;
using SudokuLib;

namespace SudokuConverterLib.Converters
{
    public static class ConverterRouter
    {
        public static IRuleConverter GetConverter(RuleType ruleType)
        {
            return GetConverter(ruleType.ToString());
        }

        public static IRuleConverter GetConverter(string ruleTypeString)
        {
            if (ruleTypeString == RuleType.Normal.ToString())
            {
                return NormalRuleConverter.GetInstance();
            }
            else if (ruleTypeString == RuleType.Cage.ToString())
            {
                return CageRuleConverter.GetInstance();
            }
            else if (ruleTypeString == RuleType.Diagonal.ToString())
            {
                return DiagonalRuleConverter.GetInstance();
            }
            else throw new InvalidDataException();
        }
    }
}
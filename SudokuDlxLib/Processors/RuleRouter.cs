using System.IO;
using SudokuLib;

namespace SudokuDlxLib.Processors
{
    public static class RuleRouter
    {
        public static RuleDlxProcessor GetRuleDlxProcessor(RuleType ruleType)
        {
            switch (ruleType)
            {
                case RuleType.Normal:
                    return new NormalRuleDlxProcessor();
                case RuleType.Cage:
                    return new CageRuleDlxProcessor();
                default:
                    throw new InvalidDataException();
            }
        }
    }
}
using System.IO;
using SudokuLib;

namespace SudokuDlxLib.Processors
{
    public static class RuleRouter
    {
        public static AbsRuleDlxProcessor GetRuleDlxProcessor(RuleType ruleType)
        {
            switch (ruleType)
            {
                case RuleType.Normal:
                    return new NormalRuleDlxProcessor();
                case RuleType.Cage:
                    return new CageRuleDlxProcessor();
                case RuleType.Diagonal:
                    return new DiagonalRuleDlxProcessor();
                default:
                    throw new InvalidDataException();
            }
        }
    }
}
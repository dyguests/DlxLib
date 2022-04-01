using System.IO;
using SudokuLib;

namespace SudokuDlxLib.Processors
{
    public static class RuleRouter
    {
        public static AbsRuleDlxProcessor GetRuleDlxProcessor(Rule rule)
        {
            switch (rule)
            {
                case NormalRule _:
                    return new NormalRuleDlxProcessor();
                case CageRule _:
                    return new CageRuleDlxProcessor();
                case DiagonalRule _:
                    return new DiagonalRuleDlxProcessor();
                default:
                    throw new InvalidDataException();
            }
        }
    }
}
using System.IO;
using PuzzleLib.Rules;

namespace SudokuDlxLib.Processors
{
    public static class RuleRouter
    {
        public static RuleDlxProcessor GetRuleDlxProcessor(Rule rule)
        {
            switch (rule)
            {
                case NormalRule _:
                    return new NormalRuleDlxProcessor();
                // case CageRule _:
                //     return new CageRuleDlxProcessor();
                // case DiagonalRule _:
                //     return new DiagonalRuleDlxProcessor();
                // case ThermometerRule _:
                //     return new ThermometerRuleDlxProcessor();
                default:
                    throw new InvalidDataException();
            }
        }
    }
}
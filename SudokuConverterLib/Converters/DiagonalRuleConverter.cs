using SudokuLib;

namespace SudokuConverterLib.Converters
{
    public class DiagonalRuleConverter : IRuleConverter
    {
        private static readonly string RulePrefix = RuleType.Diagonal.ToString();

        private static DiagonalRuleConverter sInstance;

        private DiagonalRuleConverter()
        {
        }

        public static DiagonalRuleConverter GetInstance()
        {
            return sInstance ?? (sInstance = new DiagonalRuleConverter());
        }

        public string ToDataString(Sudoku sudoku, Rule rule)
        {
            return RulePrefix;
        }

        public Rule ToRule(string content)
        {
            return new DiagonalRule();
        }
    }
}
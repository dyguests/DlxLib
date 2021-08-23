namespace SudokuLib
{
    public abstract class Rule
    {
        public RuleType type;
    }

    /// <summary>
    /// 普通数独规则
    /// </summary>
    public class NormalRule : Rule
    {
    }

    public enum RuleType
    {
        Normal,
    }
}
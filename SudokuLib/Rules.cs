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

    public class CageRule : Rule
    {
        public Cage[] cages;

        public struct Cage
        {
            public int sum;
            public int[] indexes;
        }
    }

    public enum RuleType
    {
        Normal,
        Cage,
    }
}
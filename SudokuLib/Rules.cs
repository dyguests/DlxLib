namespace SudokuLib
{
    public abstract class Rule
    {
        public abstract RuleType Type { get; }
    }

    /// <summary>
    /// 普通数独规则
    /// </summary>
    public class NormalRule : Rule
    {
        public override RuleType Type => RuleType.Normal;
    }

    public class CageRule : Rule
    {
        public Cage[] cages;

        public struct Cage
        {
            public int sum;
            public int[] indexes;
        }

        public override RuleType Type => RuleType.Cage;
    }

    public enum RuleType
    {
        Normal,
        Cage,
    }
}
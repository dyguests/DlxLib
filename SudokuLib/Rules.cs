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

    public class DiagonalRule : Rule
    {
        public override RuleType Type => RuleType.Diagonal;
    }

    public enum RuleType
    {
        /// <summary>
        /// 普通规则
        /// </summary>
        Normal,

        /// <summary>
        /// 笼子
        ///
        /// 杀手数独笼子，以及没有sum的笼子
        /// </summary>
        Cage,

        /// <summary>
        /// 对角线
        ///
        /// 对角线也不能重复
        /// </summary>
        Diagonal,

        /// <summary>
        /// 温度计
        ///
        /// 从圆圈处升序排列
        /// </summary>
        Thermometer,

        /// <summary>
        /// 箭头
        ///
        /// 箭头线上的数字和等于箭头圈的数字
        /// </summary>
        Arrow,

        /// <summary>
        /// 不等号
        ///
        /// 表示不等号两边的tile的数字一边比另一边大
        /// </summary>
        Inequality,

        /// <summary>
        /// 当前行/列中夹在1和9之间数字之和
        /// </summary>
        Sandwich,
    }
}
namespace SudokuLib
{
    public abstract class Rule
    {
    }

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

    public class DiagonalRule : Rule
    {
    }

    // public enum RuleType
    // {
    //     /// <summary>
    //     /// 普通规则
    //     /// </summary>
    //     Normal,
    //
    //     /// <summary>
    //     /// 笼子
    //     ///
    //     /// 杀手数独笼子，以及没有sum的笼子
    //     /// </summary>
    //     Cage,
    //
    //     /// <summary>
    //     /// 对角线
    //     ///
    //     /// 对角线也不能重复
    //     /// </summary>
    //     Diagonal,
    //
    //     /// <summary>
    //     /// 温度计
    //     ///
    //     /// 从圆圈处升序排列
    //     /// </summary>
    //     Thermometer,
    //
    //     /// <summary>
    //     /// 箭头
    //     ///
    //     /// 箭头线上的数字和等于箭头圈的数字
    //     /// </summary>
    //     Arrow,
    //
    //     /// <summary>
    //     /// 不等号
    //     ///
    //     /// 表示不等号两边的tile的数字一边比另一边大
    //     /// </summary>
    //     Inequality,
    //
    //     /// <summary>
    //     /// 当前行/列中夹在1和9之间数字之和
    //     /// </summary>
    //     Sandwich,
    // }
}
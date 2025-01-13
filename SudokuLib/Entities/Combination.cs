namespace SudokuLib.Entities
{
    /// <summary>
    /// 组合，升序排列
    ///
    /// 用于存放 几个tile的可能数字组合。
    ///
    /// 例： Cage(10=1+2) 表示第1个格子+第2个格子的数字和是10，则可能是组是(1,9), (2,8), ...
    ///     记做Combination(1,9), Combination(2,8), ...
    /// 这里专门抽成一个Combination类来处理这种数据，是因为存在几种特殊组合：
    /// 1、非重复组合： 不限定具体数字，仅限定数字不能相同 （用于 杀手笼子中的无数字和的笼子）
    /// 2、无限制组合： 没有限制。 （暂定，没地方用）
    /// </summary>
    public class Combination
    {
        public static Combination Unique = new Combination();
        public static Combination NoRestrictions = new Combination();

        public static Combination Of(params int[] array) => new Combination(array);

        public int[] Array { get; } = null!;

        private Combination()
        {
        }

        private Combination(int[] array)
        {
            Array = array;
        }
    }
}
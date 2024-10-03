namespace SudokuLib.Rules
{
    public class StandardRule : BaseRule
    {
        // 公共静态方法，用于获取类的唯一实例
        public static StandardRule Instance { get; } = new();

        // 私有构造函数，防止外部直接创建实例
        private StandardRule() { }
    }
}
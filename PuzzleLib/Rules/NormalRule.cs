namespace PuzzleLib.Rules
{
    /// <summary>
    /// 普通数独规则
    /// </summary>
    public class NormalRule : Rule
    {
        private static NormalRule sInstance;
        public static NormalRule Instance => sInstance ??= new NormalRule();
    }
}
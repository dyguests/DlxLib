namespace PuzzleLib.Rules
{
    /// <summary>
    /// 普通数独规则
    /// </summary>
    public class NurikabeRule : Rule
    {
        private static NurikabeRule sInstance;
        public static NurikabeRule Instance => sInstance ??= new NurikabeRule();

        private NurikabeRule() { }
    }
}
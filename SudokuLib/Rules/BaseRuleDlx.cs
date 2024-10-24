namespace SudokuLib.Rules
{
    public interface IBaseRuleDlx : IRuleDlx { }

    /// <summary>
    /// 基础规则
    ///
    /// 若 Puzzle 不包含 BaseRule，则默认添加 StandardRule
    /// </summary>
    public abstract class BaseRuleDlx : RuleDlx, IBaseRuleDlx { }
}
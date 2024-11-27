namespace SudokuLib.Rules
{
    public interface IBaseRule : IRule { }

    /// <summary>
    /// 基础规则
    ///
    /// 若 Puzzle 不包含 BaseRule，则默认添加 StandardRule
    /// </summary>
    public abstract class BaseRule : Rule, IBaseRule { }
}
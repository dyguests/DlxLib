using SudokuLib.Rules;

namespace SudokuLib.Sketchers
{
    public interface IRuleSketcher
    {
        IRule FromSketch(string sketch);
    }

    public abstract class RuleSketcher : IRuleSketcher
    {
        #region IRuleSketcher

        public abstract IRule FromSketch(string sketch);

        #endregion
    }
}
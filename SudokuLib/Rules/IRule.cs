namespace SudokuLib.Rules
{
    public interface IRule
    {
        IRule? FromSketch(string sketch);
        string ToSketch();
    }

    public abstract class Rule : IRule
    {
        #region IRuleSketcher

        public abstract IRule? FromSketch(string sketch);
        public abstract string ToSketch();

        #endregion
    }
}
namespace SudokuLib.Sketchers
{
    public interface IRuleSketcher
    {
        IRuleSketcher? FromSketch(string sketch);
        string ToSketch();
    }

    public abstract class RuleSketcher : IRuleSketcher
    {
        #region IRuleSketcher

        public abstract IRuleSketcher? FromSketch(string sketch);
        public abstract string ToSketch();

        #endregion
    }
}
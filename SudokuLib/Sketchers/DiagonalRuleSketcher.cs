using SudokuLib.Rules;

namespace SudokuLib.Sketchers
{
    public class DiagonalRuleSketcher : RuleSketcher
    {
        #region RuleSketcher

        public override IRule FromSketch(string sketch)
        {
            if (string.IsNullOrWhiteSpace(sketch)) return null;

            return null;
        }

        #endregion
    }
}
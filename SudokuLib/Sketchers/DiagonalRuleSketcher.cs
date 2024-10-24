using SudokuLib.Rules;

namespace SudokuLib.Sketchers
{
    public class DiagonalRuleSketcher : RuleSketcher
    {
        #region RuleSketcher

        public override IRuleSketcher? FromSketch(string sketch)
        {
            if (string.IsNullOrWhiteSpace(sketch)) return null;
            return sketch.StartsWith(DiagonalRuleSketch) ? new DiagonalRuleSketcher() : null;
        }

        public override string ToSketch() => DiagonalRuleSketch;

        #endregion

        #region DiagonalRuleSketcher

        private const string DiagonalRuleSketch = "Diagonal";

        #endregion
    }
}
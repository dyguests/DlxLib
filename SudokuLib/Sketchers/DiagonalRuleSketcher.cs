using SudokuLib.Rules;

namespace SudokuLib.Sketchers
{
    public class DiagonalRuleSketcher : RuleSketcher
    {
        #region RuleSketcher

        public override IRule FromSketch(string sketch)
        {
            if (string.IsNullOrWhiteSpace(sketch)) return null;
            return sketch.StartsWith(DiagonalRuleSketch) ? new DiagonalRule() : null;
        }

        public override string ToSketch(IRule rule) => rule is DiagonalRule ? DiagonalRuleSketch : null;

        #endregion

        #region DiagonalRuleSketcher

        private const string DiagonalRuleSketch = "Diagonal";

        #endregion
    }
}
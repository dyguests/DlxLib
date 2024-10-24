namespace SudokuLib.Rules
{
    public class DiagonalRule : Rule
    {
        #region RuleSketcher

        public override IRule? FromSketch(string sketch)
        {
            if (string.IsNullOrWhiteSpace(sketch)) return null;
            return sketch.StartsWith(DiagonalRuleSketch) ? new DiagonalRule() : null;
        }

        public override string ToSketch() => DiagonalRuleSketch;

        #endregion

        #region DiagonalRuleSketcher

        private const string DiagonalRuleSketch = "Diagonal";

        #endregion
    }
}
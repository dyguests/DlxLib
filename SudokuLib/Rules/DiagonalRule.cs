namespace SudokuLib.Rules
{
    public class DiagonalRule : Rule
    {
        #region Rule

        public override IRule? FromSketch(string sketch)
        {
            if (string.IsNullOrWhiteSpace(sketch)) return null;
            return sketch.StartsWith(RulePrefix) ? new DiagonalRule() : null;
        }

        public override string ToSketch() => RulePrefix;

        #endregion

        #region DiagonalRule

        private const string RulePrefix = "Diagonal";

        #endregion
    }
}
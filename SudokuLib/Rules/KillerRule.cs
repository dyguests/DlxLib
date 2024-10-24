namespace SudokuLib.Rules
{
    /// <summary>
    /// Sample:
    /// Killer sum=index1+index2;3=12+13;0=1+2
    /// </summary>
    public class KillerRule : Rule
    {
        #region Rule

        public override IRule? FromSketch(string sketch)
        {
            if (string.IsNullOrWhiteSpace(sketch)) return null;
            if (!sketch.StartsWith(KillerRulePrefix)) return null;
            var cagesSketch = sketch.Substring(KillerRulePrefix.Length).Trim();
            return new DiagonalRule();
        }

        public override string ToSketch()
        {
            return KillerRulePrefix; // todo + ...
        }

        #endregion

        #region KillerRule

        private const string KillerRulePrefix = "Killer";

        #endregion
    }
}
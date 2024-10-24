﻿namespace SudokuLib.Rules
{
    public class DiagonalRule : Rule
    {
        #region Rule

        public override IRule? FromSketch(string sketch)
        {
            if (string.IsNullOrWhiteSpace(sketch)) return null;
            return sketch.StartsWith(DiagonalRulePrefix) ? new DiagonalRule() : null;
        }

        public override string ToSketch() => DiagonalRulePrefix;

        #endregion

        #region DiagonalRule

        private const string DiagonalRulePrefix = "Diagonal";

        #endregion
    }
}
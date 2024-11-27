using System;
using System.Linq;

namespace SudokuLib.Rules
{
    /// <summary>
    /// 对比点约束
    /// </summary>
    public class KropkiRule : Rule
    {
        #region Rule

        public override IRule? FromSketch(string sketch)
        {
            if (string.IsNullOrWhiteSpace(sketch)) return null;
            if (!sketch.StartsWith(RulePrefix)) return null;
            var contentSketch = sketch.Substring(RulePrefix.Length).Trim();
            contentSketch.Split(KropkiSeparator)
                .Select(item => item.Trim())
                .SkipWhile(string.IsNullOrEmpty)
                ;
            return null;
        }

        public override string ToSketch()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region KropkiRule

        private const string RulePrefix = "Kropki";
        private const string KropkiSeparator = ";";

        /// <summary>
        /// 白点约束：表示相邻格子的数字是连续的
        /// </summary>
        public class Consecutive { }

        /// <summary>
        /// 黑点约束：表示相邻格子的数字存在倍数关系
        /// </summary>
        public class Ratio { }

        #endregion
    }
}
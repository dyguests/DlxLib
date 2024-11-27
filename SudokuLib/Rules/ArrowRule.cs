using System.Linq;
using System.Text;

namespace SudokuLib.Rules
{
    /// <summary>
    /// 箭头指向的格子中的数字之和等于箭头起始格的数字。
    /// 
    /// Sample:
    /// Arrow index1=index2+index3;3=12+13;0=1+2
    /// </summary>
    public class ArrowRule : Rule
    {
        #region Rule

        public override IRule? FromSketch(string sketch)
        {
            if (string.IsNullOrWhiteSpace(sketch)) return null;
            if (!sketch.StartsWith(RulePrefix)) return null;
            var contentSketch = sketch.Substring(RulePrefix.Length).Trim();
            var arrows = contentSketch.Split(ArrowSeparator)
                .Select(str => str.Trim())
                .SkipWhile(string.IsNullOrEmpty)
                .Select(Arrow.FormSketch)
                .Where(item => item != null)
                .ToArray();
            return new ArrowRule(arrows);
        }

        public override string ToSketch()
        {
            var sb = new StringBuilder();
            sb.Append(RulePrefix).Append(" ");
            for (var i = 0; i < _arrows.Length; i++)
            {
                var arrow = _arrows[i];
                sb.Append(arrow.ToSketch());
                if (i < _arrows.Length - 1)
                {
                    sb.Append(ArrowSeparator);
                }
            }

            return sb.ToString();
        }

        #endregion

        #region ArrowRule

        private const string RulePrefix = "Arrow";
        private const string ArrowSeparator = ";";

        private readonly Arrow[] _arrows;
        public string[] ReadonlyArrow => (string[])_arrows.Clone();

        private ArrowRule(params Arrow[] arrows)
        {
            _arrows = arrows;
        }

        public class Arrow
        {
            public int SumIndex { get; }
            public int[] ArrowIndexes { get; }

            #region Arrow

            private Arrow(int sumIndex, int[] arrowIndexes)
            {
                SumIndex = sumIndex;
                ArrowIndexes = arrowIndexes;
            }

            public static Arrow FormSketch(string sketch)
            {
                var parts = sketch.Split("=");
                if (parts.Length != 2) return null;
                if (!int.TryParse(parts[0], out var sumIndex)) return null;
                var indexStrings = parts[1].Split("+");
                if (indexStrings.Length < 1) return null;
                var indexes = indexStrings
                    .Select(indexString => int.TryParse(indexString, out var index) ? index : (int?)null)
                    .Where(index => index.HasValue)
                    .Select(index => index.Value)
                    .ToArray();
                if (indexes.Length < 1) return null;
                return new Arrow(sumIndex, indexes);
            }

            public string ToSketch()
            {
                return SumIndex + "=" + string.Join("+", ArrowIndexes);
            }

            #endregion
        }

        #endregion
    }
}
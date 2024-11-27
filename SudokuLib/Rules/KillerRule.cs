using System;
using System.Linq;
using System.Text;

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
            if (!sketch.StartsWith(RulePrefix)) return null;
            var contentSketch = sketch.Substring(RulePrefix.Length).Trim();
            var cages = contentSketch.Split(CageSeparator)
                .Select(str => str.Trim())
                .SkipWhile(string.IsNullOrEmpty)
                .Select(Cage.Instance.FromSketch)
                .Where(item => item != null)
                .Select(rule => rule as Cage)
                .Select(rule => rule!)
                .ToArray();
            return new KillerRule(cages);
        }

        public override string ToSketch()
        {
            var sb = new StringBuilder();
            sb.Append(RulePrefix).Append(" ");
            for (var i = 0; i < _cages.Length; i++)
            {
                var cage = _cages[i];
                sb.Append(cage.ToSketch());
                if (i < _cages.Length - 1)
                {
                    sb.Append(CageSeparator);
                }
            }

            return sb.ToString();
        }

        #endregion

        #region KillerRule

        private const string RulePrefix = "Killer";
        private const string CageSeparator = ";";

        public static KillerRule Default => new();

        private readonly Cage[] _cages;
        public Cage[] ReadonlyCages => (Cage[])_cages.Clone();

        public KillerRule(params Cage[] cages)
        {
            _cages = cages;
        }

        public class Cage : Rule
        {
            #region Cage

            public static Cage Instance { get; } = new(0, Array.Empty<int>());

            public int Sum { get; }
            public int[] Indexes { get; }

            public Cage(int sum, int[] indexes)
            {
                Sum = sum;
                Indexes = indexes;
            }

            /// <summary>
            /// Sample:
            /// 3=12+13
            /// 0=1+2
            /// </summary>
            /// <param name="sketch"></param>
            /// <returns></returns>
            public override IRule? FromSketch(string sketch)
            {
                var parts = sketch.Split("=");
                if (parts.Length != 2) return null;
                if (!int.TryParse(parts[0], out var sum)) return null;
                var indexStrings = parts[1].Split("+");
                if (indexStrings.Length < 1) return null;
                var indexes = indexStrings
                    .Select(indexString => int.TryParse(indexString, out var index) ? index : (int?)null)
                    .Where(index => index.HasValue)
                    .Select(index => index!.Value)
                    .ToArray();
                if (indexes.Length < 1) return null;
                return new Cage(sum, indexes);
            }

            public override string ToSketch()
            {
                return Sum + "=" + string.Join("+", Indexes);
            }

            #endregion

            public override string ToString() => $"Cage({Sum}={string.Join("+", Indexes)})";
        }

        #endregion
    }
}
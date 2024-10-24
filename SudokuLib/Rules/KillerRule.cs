using System.Linq;

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
            var cages = cagesSketch.Split(";")
                .Select(cageSketch => cageSketch.Trim())
                .Select(Cage.FormSketch)
                .Where(cage => cage != null)
                .ToArray();
            return new KillerRule(cages);
        }

        public override string ToSketch()
        {
            return KillerRulePrefix; // todo + ...
        }

        #endregion

        #region KillerRule

        private const string KillerRulePrefix = "Killer";

        private readonly Cage[] _cages;
        public Cage[] ReadonlyCages => (Cage[])_cages.Clone();

        public KillerRule(params Cage[] cages)
        {
            _cages = cages;
        }

        public class Cage
        {
            public int Sum { get; }
            public int[] Indexes { get; }

            private Cage(int sum, int[] indexes)
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
            public static Cage FormSketch(string sketch)
            {
                var parts = sketch.Split("=");
                if (parts.Length != 2) return null;
                if (!int.TryParse(parts[0], out var sum)) return null;
                var indexStrings = parts[1].Split("+");
                if (indexStrings.Length < 1) return null;
                var indexes = indexStrings
                    .Select(indexString => int.TryParse(indexString, out var index) ? index : (int?)null)
                    .Where(index => index.HasValue)
                    .Select(index => index.Value)
                    .ToArray();
                if (indexes.Length < 1) return null;
                return new Cage(sum, indexes);
            }
        }

        #endregion
    }
}
using System.Linq;
using System.Text;

namespace SudokuLib.Rules
{
    public class ThermoRule : Rule
    {
        #region Rule

        /// <summary>
        /// 温度计规则
        /// 
        /// Sample:
        /// Thermo index1<index2<index3;1<2<10
        /// </summary>
        /// <param name="sketch"></param>
        /// <returns></returns>
        public override IRule? FromSketch(string sketch)
        {
            if (string.IsNullOrWhiteSpace(sketch)) return null;
            if (!sketch.StartsWith(RulePrefix)) return null;
            var contentSketch = sketch.Substring(RulePrefix.Length).Trim();
            var thermos = contentSketch.Split(ThermoSeparator)
                .Select(str => str.Trim())
                .SkipWhile(string.IsNullOrEmpty)
                .Select(Thermo.FromSketch)
                .Where(item => item != null)
                .ToArray();

            return new ThermoRule(thermos);
        }

        public override string ToSketch()
        {
            var sb = new StringBuilder();
            sb.Append(RulePrefix).Append(" ");
            for (var i = 0; i < _thermos.Length; i++)
            {
                var thermo = _thermos[i];
                sb.Append(thermo.ToSketch());
                if (i < _thermos.Length - 1)
                {
                    sb.Append(ThermoSeparator);
                }
            }

            return sb.ToString();
        }

        #endregion

        #region ThermoRule

        private const string RulePrefix = "Thermo";
        private const string ThermoSeparator = ";";

        private readonly Thermo[] _thermos;
        public Thermo[] ReadonlyThermos => (Thermo[])_thermos.Clone();

        private ThermoRule(params Thermo[] thermos)
        {
            _thermos = thermos;
        }

        public class Thermo
        {
            public int[] Indexes { get; }

            private Thermo(int[] indexes)
            {
                Indexes = indexes;
            }

            public static Thermo FromSketch(string sketch)
            {
                var parts = sketch.Split("<");
                if (parts.Length < 2) return null;
                var indexes = parts
                    .Select(indexString => int.TryParse(indexString, out var index) ? index : (int?)null)
                    .Where(index => index.HasValue)
                    .Select(index => index.Value)
                    .ToArray();
                if (indexes.Length < 2) return null;
                return new Thermo(indexes);
            }

            public string ToSketch()
            {
                return string.Join("<", Indexes);
            }
        }

        #endregion
    }
}
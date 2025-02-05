using System;
using System.Linq;
using System.Text;

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
            var kropkis = contentSketch.Split(KropkiSeparator)
                .Select(item => item.Trim())
                .SkipWhile(string.IsNullOrEmpty)
                .Select(aSketch => Consecutive.Instance.FromSketch(aSketch) ?? Ratio.Instance.FromSketch(aSketch))
                .Where(item => item != null)
                .Select(rule => rule as Kropki)
                .Select(rule => rule!)
                .ToArray();
            return new KropkiRule(kropkis);
        }

        public override string ToSketch()
        {
            var sb = new StringBuilder();
            sb.Append(RulePrefix).Append(" ");
            for (var i = 0; i < _items.Length; i++)
            {
                var cage = _items[i];
                sb.Append(cage.ToSketch());
                if (i < _items.Length - 1)
                {
                    sb.Append(KropkiSeparator);
                }
            }

            return sb.ToString();
        }

        #endregion

        #region KropkiRule

        private const string RulePrefix = "Kropki";
        private const string KropkiSeparator = ";";

        private readonly Kropki[] _items;
        public Kropki[] ReadonlyItems => (Kropki[])_items.Clone();

        private KropkiRule(params Kropki[] items)
        {
            _items = items;
        }

        public abstract class Kropki : Rule { }

        /// <summary>
        /// 白点约束：表示相邻格子的数字是连续的
        /// </summary>
        public class Consecutive : Kropki
        {
            public static Consecutive Instance { get; } = new Consecutive();

            public override IRule? FromSketch(string sketch)
            {
                throw new NotImplementedException();
            }

            public override string ToSketch()
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 黑点约束：表示相邻格子的数字存在倍数关系
        /// </summary>
        public class Ratio : Kropki
        {
            public static Ratio Instance { get; } = new Ratio();

            public override IRule? FromSketch(string sketch)
            {
                throw new NotImplementedException();
            }

            public override string ToSketch()
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
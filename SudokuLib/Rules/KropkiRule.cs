using System.Linq;
using System.Text;

namespace SudokuLib.Rules
{
    /// <summary>
    /// 对比点约束
    ///
    /// 主要用来约束两个相邻的格子之间的关系。
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

        /// <summary>
        /// 点约束
        /// </summary>
        public abstract class Kropki : Rule { }

        /// <summary>
        /// 白点约束：表示相邻格子的数字是连续的
        /// </summary>
        public class Consecutive : Kropki
        {
            public static Consecutive Instance { get; } = new Consecutive();

            /// <summary>
            /// 12c 表示 12 和它右边的格子是连续的
            /// </summary>
            private const string Right = "c";

            /// <summary>
            /// 12C 表示 12 和它上边的格子是连续的
            /// </summary>
            private const string Up = "C";

            public int Index { get; protected set; }

            /// <summary>
            /// 0: Right
            /// 1: Up
            /// 2: Left
            /// 3: Bottom
            /// </summary>
            public int Direction { get; protected set; }

            public override IRule? FromSketch(string sketch)
            {
                if (sketch == null) return null;

                int direction;
                if (sketch.EndsWith(Right)) direction = 0;
                else if (sketch.EndsWith(Up)) direction = 1;
                else return null;

                if (!int.TryParse(sketch[..^1], out var index)) return null;
                return new Consecutive
                {
                    Index = index,
                    Direction = direction
                };
            }

            public override string ToSketch()
            {
                return $"{Index}{(Direction == 0 ? Right : Up)}";
            }
        }

        /// <summary>
        /// 黑点约束：表示相邻格子的数字存在倍数关系
        /// </summary>
        public class Ratio : Kropki
        {
            public static Ratio Instance { get; } = new Ratio();

            private const string Right = "r";
            private const string Up = "R";

            public int Index { get; protected set; }

            /// <summary>
            /// 0: Right
            /// 1: Up
            /// 2: Left
            /// 3: Bottom
            /// </summary>
            public int Direction { get; protected set; }

            public override IRule? FromSketch(string sketch)
            {
                if (sketch == null) return null;

                int direction;
                if (sketch.EndsWith(Right)) direction = 0;
                else if (sketch.EndsWith(Up)) direction = 1;
                else return null;

                if (!int.TryParse(sketch[..^1], out var index)) return null;
                return new Ratio
                {
                    Index = index,
                    Direction = direction
                };
            }

            public override string ToSketch()
            {
                return $"{Index}{(Direction == 0 ? Right : Up)}";
            }
        }

        #endregion
    }
}
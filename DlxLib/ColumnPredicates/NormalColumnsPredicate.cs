using System.Linq;

namespace DlxLib.ColumnPredicates
{
    public class NormalColumnsPredicate : ColumnPredicate
    {
        private readonly int[] _primaryColumns;
        private readonly int[] _secondaryColumns;

        public NormalColumnsPredicate(int[] primaryColumns, int[] secondaryColumns)
        {
            this._primaryColumns = primaryColumns;
            this._secondaryColumns = secondaryColumns;
        }

        public override bool IsPrimaryColumn(int column) => _primaryColumns.Contains(column);
        public override bool IsSecondaryColumn(int column) => _secondaryColumns.Contains(column);
    }
}
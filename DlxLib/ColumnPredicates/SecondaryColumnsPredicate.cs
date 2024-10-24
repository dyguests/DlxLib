using System.Linq;

namespace DlxLib.ColumnPredicates
{
    public class SecondaryColumnsPredicate : ColumnPredicate
    {
        private readonly int[] _secondaryColumns;

        public SecondaryColumnsPredicate(int[] secondaryColumns)
        {
            this._secondaryColumns = secondaryColumns;
        }

        public override bool IsPrimaryColumn(int column) => !_secondaryColumns.Contains(column);
        public override bool IsSecondaryColumn(int column) => _secondaryColumns.Contains(column);
    }
}
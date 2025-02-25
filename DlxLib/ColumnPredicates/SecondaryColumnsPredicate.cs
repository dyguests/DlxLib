using System;
using System.Linq;

namespace DlxLib.ColumnPredicates
{
    [Obsolete]
    public class SecondaryColumnsPredicate : ColumnPredicate
    {
        #region ColumnPredicate

        public override int[] GetColumnPredicate(int length)
        {
            var columnPredicate = new int[length];
            Array.Fill(columnPredicate, KeyPrimaryColumn);
            for (var i = 0; i < length; i++)
            {
                columnPredicate[i] = KeySecondaryColumn;
            }

            return columnPredicate;
        }

        public override bool IsPrimaryColumn(int column) => !_secondaryColumns.Contains(column);
        public override bool IsSecondaryColumn(int column) => _secondaryColumns.Contains(column);

        #endregion

        #region SecondaryColumnsPredicate

        private readonly int[] _secondaryColumns;

        public SecondaryColumnsPredicate(int[] secondaryColumns)
        {
            this._secondaryColumns = secondaryColumns;
        }

        #endregion
    }
}
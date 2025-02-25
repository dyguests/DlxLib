using System;
using System.Linq;

namespace DlxLib.ColumnPredicates
{
    public class NormalColumnsPredicate : ColumnPredicate
    {
        #region ColumnPredicate

        public override int[] GetColumnPredicate(int length)
        {
            var result = new int[length];
            Array.Fill(result, KeyHintColumn); // Fill with 2 (KeyHintColumn) first
            foreach (var column in _secondaryColumns)
            {
                if (column < length) result[column] = KeySecondaryColumn;
            }

            foreach (var column in _primaryColumns)
            {
                if (column < length) result[column] = KeyPrimaryColumn;
            }

            return result;
        }

        public override bool IsPrimaryColumn(int column) => _primaryColumns.Contains(column);
        public override bool IsSecondaryColumn(int column) => _secondaryColumns.Contains(column);

        #endregion

        #region NormalColumnsPredicate

        private readonly int[] _primaryColumns;
        private readonly int[] _secondaryColumns;

        public NormalColumnsPredicate(int[] primaryColumns, int[] secondaryColumns)
        {
            this._primaryColumns = primaryColumns;
            this._secondaryColumns = secondaryColumns;
        }

        #endregion
    }
}
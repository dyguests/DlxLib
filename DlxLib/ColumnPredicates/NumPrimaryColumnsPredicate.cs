using System;

namespace DlxLib.ColumnPredicates
{
    public class NumPrimaryColumnsPredicate : ColumnPredicate
    {
        #region ColumnPredicate

        public override int[] GetColumnPredicate(int length)
        {
            var columnPredicate = new int[length];
            Array.Fill(columnPredicate, KeySecondaryColumn);
            for (var i = 0; i < length; i++)
            {
                columnPredicate[i] = KeyPrimaryColumn;
            }

            return columnPredicate;
        }

        public override bool IsPrimaryColumn(int column) => column < _numPrimaryColumns;
        public override bool IsSecondaryColumn(int column) => column >= _numPrimaryColumns;

        #endregion

        #region NumPrimaryColumnsPredicate

        private readonly int _numPrimaryColumns;

        public NumPrimaryColumnsPredicate(int numPrimaryColumns)
        {
            this._numPrimaryColumns = numPrimaryColumns;
        }

        #endregion
    }
}
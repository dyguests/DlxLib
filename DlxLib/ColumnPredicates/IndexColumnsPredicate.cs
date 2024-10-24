namespace DlxLib.ColumnPredicates
{
    public class IndexColumnsPredicate : ColumnPredicate
    {
        private const int KeyPrimaryColumn = 0;
        private const int KeySecondaryColumn = 1;
        // private const int KeyHintColumn = 2;

        /// <summary>
        /// sample: new[] { 1, 1, 0, 0 }
        /// </summary>
        private readonly int[] _columnPredicate;

        #region ColumnPredicate

        public override bool IsPrimaryColumn(int column)
        {
            return column < _columnPredicate.Length && _columnPredicate[column] == KeyPrimaryColumn;
        }

        public override bool IsSecondaryColumn(int column)
        {
            return column < _columnPredicate.Length && _columnPredicate[column] == KeySecondaryColumn;
        }

        #endregion

        #region IndexColumnsPredicate

        public IndexColumnsPredicate(int[] columnPredicate)
        {
            _columnPredicate = columnPredicate;
        }

        #endregion
    }
}
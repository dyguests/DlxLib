namespace DlxLib.ColumnPredicates
{
    public class IndexColumnsPredicate : ColumnPredicate
    {
        /// <summary>
        /// sample: new[] { 1, 1, 0, 0 }
        /// </summary>
        private readonly int[] _columnPredicate;

        #region ColumnPredicate

        public override bool IsPrimaryColumn(int column)
        {
            return column < _columnPredicate.Length && _columnPredicate[column] == 1;
        }

        public override bool IsSecondaryColumn(int column)
        {
            return column < _columnPredicate.Length && _columnPredicate[column] == 0;
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
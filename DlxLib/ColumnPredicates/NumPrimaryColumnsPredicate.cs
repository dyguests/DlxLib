namespace DlxLib.ColumnPredicates
{
    public class NumPrimaryColumnsPredicate : ColumnPredicate
    {
        private readonly int _numPrimaryColumns;

        public NumPrimaryColumnsPredicate(int numPrimaryColumns)
        {
            this._numPrimaryColumns = numPrimaryColumns;
        }

        public override bool IsPrimaryColumn(int column) => column < _numPrimaryColumns;
        public override bool IsSecondaryColumn(int column) => column > _numPrimaryColumns;
    }
}
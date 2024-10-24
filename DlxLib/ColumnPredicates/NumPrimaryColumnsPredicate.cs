namespace DlxLib.ColumnPredicates
{
    public class NumPrimaryColumnsPredicate : IColumnPredicate
    {
        private readonly int numPrimaryColumns;

        public NumPrimaryColumnsPredicate(int numPrimaryColumns)
        {
            this.numPrimaryColumns = numPrimaryColumns;
        }

        public bool IsPrimaryColumn(int column) => column <= numPrimaryColumns;

        public bool IsSecondaryColumn(int column) => column > numPrimaryColumns;
    }
}
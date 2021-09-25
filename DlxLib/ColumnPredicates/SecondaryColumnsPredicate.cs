using System.Linq;

namespace DlxLib.ColumnPredicates
{
    public class SecondaryColumnsPredicate : IColumnPredicate
    {
        private readonly int[] secondaryColumns;

        public SecondaryColumnsPredicate(int[] secondaryColumns)
        {
            this.secondaryColumns = secondaryColumns;
        }

        public bool IsPrimaryColumn(int column) => !secondaryColumns.Contains(column);

        public bool IsSecondaryColumn(int column) => secondaryColumns.Contains(column);
    }
}
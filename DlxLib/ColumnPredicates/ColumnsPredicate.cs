using System.Linq;

namespace DlxLib.ColumnPredicates
{
    public class ColumnsPredicate : IColumnPredicate
    {
        private readonly int[] primaryColumns;
        private readonly int[] secondaryColumns;

        public ColumnsPredicate(int[] primaryColumns, int[] secondaryColumns)
        {
            this.primaryColumns = primaryColumns;
            this.secondaryColumns = secondaryColumns;
        }

        public bool IsPrimaryColumn(int column) => primaryColumns.Contains(column);

        public bool IsSecondaryColumn(int column) => secondaryColumns.Contains(column);
    }
}
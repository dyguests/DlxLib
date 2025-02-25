namespace DlxLib.ColumnPredicates
{
    /// <summary>
    /// 判断 主列、副列、提示列
    /// </summary>
    public interface IColumnPredicate
    {
        string? Name { get; }
        int[] GetColumnPredicate(int length);

        bool IsPrimaryColumn(int column);
        bool IsSecondaryColumn(int column);
    }

    public abstract class ColumnPredicate : IColumnPredicate
    {
        public const int KeyPrimaryColumn = 0;
        public const int KeySecondaryColumn = 1;
        public const int KeyHintColumn = 2;

        #region IColumnPredicate

        public virtual string? Name => null;
        public abstract int[] GetColumnPredicate(int length);
        public abstract bool IsPrimaryColumn(int column);
        public abstract bool IsSecondaryColumn(int column);

        #endregion
    }
}
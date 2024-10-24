namespace DlxLib.ColumnPredicates
{
    /// <summary>
    /// 判断 主列、副列、提示列
    /// </summary>
    public interface IColumnPredicate
    {
        string? Name { get; }

        bool IsPrimaryColumn(int column);
        bool IsSecondaryColumn(int column);
    }

    public abstract class ColumnPredicate : IColumnPredicate
    {
        #region IColumnPredicate

        public virtual string? Name => null;
        public abstract bool IsPrimaryColumn(int column);
        public abstract bool IsSecondaryColumn(int column);

        #endregion
    }
}
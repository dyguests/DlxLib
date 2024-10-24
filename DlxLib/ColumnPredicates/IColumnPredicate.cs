namespace DlxLib.ColumnPredicates
{
    /// <summary>
    /// 判断 主列、副列、提示列
    /// </summary>
    public interface IColumnPredicate
    {
        bool IsPrimaryColumn(int column);
        bool IsSecondaryColumn(int column);
    }
}
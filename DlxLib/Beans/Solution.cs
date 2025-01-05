namespace DlxLib.Beans
{
    /// <summary>
    /// Dlx 的解
    /// </summary>
    public struct Solution
    {
        public int[] RowIndexes { get; set; }

        /// <summary>
        /// 搜索深度
        /// </summary>
        public int Deep { get; set; }
    }
}
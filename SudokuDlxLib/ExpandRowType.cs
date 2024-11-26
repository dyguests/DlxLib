namespace SudokuDlxLib
{
    public enum ExpandRowType
    {
        /// <summary>
        /// 顺序扩展
        ///
        /// 例：比如一个空位的输入可能是1，2，3，顺序扩展就是先填1，再填2，再填3。
        /// 这样能保证每次填充的结果都是一样的。
        /// </summary>
        Sequence,
        /// <summary>
        /// 随机扩展
        ///
        /// 例：比如一个空格的输入可能是1，2，3，随机扩展就是随机填充1，2，3，可能是132，也可能是312，等等。
        /// 这样每次填充的结果都可能是不一样的。
        /// </summary>
        Random,
    }
}
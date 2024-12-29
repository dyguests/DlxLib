namespace SudokuLib
{
    public static class ColumnPredicateEx
    {
        /// <summary>
        /// position 提示列
        ///
        /// 该提示列显示 Puzzle.Digit.Index
        /// </summary>
        public const int KeyPosition = 81;

        /// <summary>
        /// 可能数字提示列
        /// 
        /// 用来表示 当前行 可能的输入数字 1~9
        /// 用 0b111_111_111 来表示
        /// 例如
        /// 0b000_000_001表示只能输入1
        /// 0b000_000_011表示只能输入1或2
        /// </summary>
        public const int KeyPossibleColumn = 9;
    }
}
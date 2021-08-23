namespace SudokuLib
{
    /// <summary>
    /// 一个9*9的数独迷题的数据结构
    /// </summary>
    public struct Sudoku
    {
        /// <summary>
        /// 1~9 or empty(0)
        /// </summary>
        public int[] initNumbers;

        public int[] solutionNumbers;

        public Rule[] rules;
    }
}
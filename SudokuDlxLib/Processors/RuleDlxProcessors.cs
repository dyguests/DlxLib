using SudokuLib;

namespace SudokuDlxLib.Processors
{
    public abstract class RuleDlxProcessor
    {
        /// <summary>
        /// 消减可能的数字
        /// </summary>
        /// <param name="sudoku"></param>
        /// <param name="possibleNumbersIndexes"></param>
        public abstract void ReducePossibleNumbers(Sudoku sudoku, int[][] possibleNumbersIndexes);

        public abstract RuleMatrix RuleToMatrix(Sudoku sudoku, int[][] possibleNumbersIndexes);
        public abstract int[] SolutionToNumbers(int[,] matrix, int[] solution);
    }
}
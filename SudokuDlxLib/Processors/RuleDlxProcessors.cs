using System;
using PuzzleLib;

namespace SudokuDlxLib.Processors
{
    public abstract class RuleDlxProcessor
    {
        protected const int TileCount = 9 * 9;
        protected const int NumberCount = 9;

        /// <summary>
        /// 消减可能的数字
        /// </summary>
        /// <param name="sudoku"></param>
        /// <param name="possibleNumbersIndexes"></param>
        public abstract void ReducePossibleNumbers(Sudoku sudoku, int[][] possibleNumbersIndexes);

        public abstract RuleMatrix RuleToMatrix(Sudoku sudoku, int[][] possibleNumbersIndexes);
        public virtual int[] SolutionToNumbers(int[,] matrix, int[] solution) => throw new NotImplementedException();
    }
}
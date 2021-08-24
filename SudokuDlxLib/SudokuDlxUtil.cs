using System.Linq;
using SudokuDlxLib.Processors;
using SudokuDlxLib.Utils;
using SudokuLib;

namespace SudokuDlxLib
{
    public static class SudokuDlxUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sudoku"></param>
        /// <returns>(matrix, secondaryColumns)</returns>
        public static (int[,] matrix, int[] secondaryColumns) SudokuToMatrix(Sudoku sudoku)
        {
            var ruleMatrixs = sudoku.rules.Select(rule => RuleRouter.GetRuleDlxProcessor(rule.type).RuleToMatrix(sudoku, rule));

            return (ruleMatrixs.First().matrix, new int[0]);
        }

        public static int[] SolutionToNumbers(Sudoku sudoku, int[,] matrix, int[] solution)
        {
            return sudoku.rules.First().Let(rule => RuleRouter.GetRuleDlxProcessor(rule.type)).SolutionToNumbers(matrix, solution);
        }
    }

    public struct RuleMatrix
    {
        public RuleType type;
        public int[,] matrix;
    }
}
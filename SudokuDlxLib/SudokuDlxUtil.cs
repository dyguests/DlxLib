using System.Linq;
using SudokuDlxLib.Generators;
using SudokuLib;

namespace SudokuDlxLib
{
    public static class SudokuDlxUtil
    {
        public static int[,] SudokuToMatrix(Sudoku sudoku)
        {
            var ruleMatrixs = sudoku.rules.Select(rule => RuleRouter.GetRuleMatrixGenerator(rule.type).RuleToMatrix(sudoku, rule));

            return ruleMatrixs.First().matrix;
        }
    }

    public struct RuleMatrix
    {
        public RuleType type;
        public int[,] matrix;
    }
}
using System;
using SudokuLib;

namespace SudokuDlxLib.Processors
{
    public class ThermometerRuleDlxProcessor:RuleDlxProcessor
    {
        public override void ReducePossibleNumbers(Sudoku sudoku, int[][] possibleNumbersIndexes)
        {
            throw new NotImplementedException();
        }

        public override RuleMatrix RuleToMatrix(Sudoku sudoku, int[][] possibleNumbersIndexes)
        {
            throw new System.NotImplementedException();
        }
    }
}
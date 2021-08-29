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
        /// <returns>(matrix, primaryColumns, secondaryColumns)</returns>
        public static SudokuMatrix SudokuToMatrix(Sudoku sudoku)
        {
            // 取得所有处理器
            var ruleDlxProcessors = sudoku.rules.Select(rule => RuleRouter.GetRuleDlxProcessor(rule.Type)).ToList();
            var possibleNumbersIndexes = Enumerable.Range(0, 81).Select(index => new[] {1, 2, 3, 4, 5, 6, 7, 8, 9}).ToArray();
            ruleDlxProcessors.ForEach(processor => processor.ReducePossibleNumbers(sudoku, possibleNumbersIndexes));
            var ruleMatrices = ruleDlxProcessors.Select(processor => processor.RuleToMatrix(sudoku, possibleNumbersIndexes));
            var sudokuMatrix = new Matrix();
            ruleMatrices.ForEach(matrix => sudokuMatrix.Expand(matrix));
            return new SudokuMatrix
            {
                matrix = sudokuMatrix.matrix,
                primaryColumns = sudokuMatrix.primaryColumns,
                secondaryColumns = sudokuMatrix.secondaryColumns
            };
        }

        public static int[] SolutionToNumbers(Sudoku sudoku, int[,] matrix, int[] solution)
        {
            return sudoku.rules.First().Let(rule => RuleRouter.GetRuleDlxProcessor(rule.Type)).SolutionToNumbers(matrix, solution);
        }
    }

    public class Matrix
    {
        /// matrix中分为primaryColumns和secondaryColumns以及hintColumns。
        /// 其中hintColumns就是不在primaryColumns和secondaryColumns的列。
        /// hintColumns不参与dlx运算，但是可以用于帮助多个矩阵进行连接，以及对solution的处理。
        /// </summary>
        public int[,] matrix;

        public int[] primaryColumns;
        public int[] secondaryColumns;

        public void Deconstruct(out int[,] matrix, out int[] primaryColumns, out int[] secondaryColumns) => (matrix, primaryColumns, secondaryColumns) = (this.matrix, this.primaryColumns, this.secondaryColumns);
    }

    public class SudokuMatrix : Matrix
    {
        // /// <summary>
        // /// matrix中分为primaryColumns和secondaryColumns以及hintColumns。
        // /// 其中hintColumns就是不在primaryColumns和secondaryColumns的列。
        // /// hintColumns不参与dlx运算，但是可以用于帮助多个矩阵进行连接，以及对solution的处理。
        // /// </summary>
        // public int[,] matrix;
        //
        // public int[] primaryColumns;
        // public int[] secondaryColumns;
        //
        // public void Deconstruct(out int[,] matrix, out int[] primaryColumns, out int[] secondaryColumns) => (matrix, primaryColumns, secondaryColumns) = (this.matrix, this.primaryColumns, this.secondaryColumns);
    }

    public class RuleMatrix : SudokuMatrix
    {
        public RuleType type;
    }
}
using System.Collections.Generic;
using System.Linq;
using SudokuLib;

namespace SudokuDlxLib
{
    public static class SudokuDlxUtil
    {
        #region ToMatrix

        public static int[,] ToMatrix(IPuzzle puzzle)
        {
            var rows = puzzle.Rules.Aggregate(CreatePositionRows(puzzle.Digits.Length), (current, rule) => rule.ExpandRows(current, puzzle));
            return rows.RowsToMatrix();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns>按position升序的IEnumerable</returns>
        private static IEnumerable<int[]> CreatePositionRows(int size)
        {
            return Enumerable.Range(0, size).Select(i =>
            {
                var row = new int[size];
                row[i] = 1;
                return row;
            });
        }

        #endregion

        #region ToSolution

        public static int[] ToSolution(IPuzzle puzzle, int[,] matrix, int[] rowIndexes)
        {
            var solution = (int[])puzzle.Digits.Clone();
            var rows = matrix.MatrixToRows().ToList();
            var resultRows = rowIndexes.Select(rowIndex => rows[rowIndex]).ToList();
            foreach (var rule in puzzle.Rules)
            {
                if (rule.FillSolution(solution, resultRows, puzzle)) break;
            }
            return solution;
        }

        #endregion
    }
}
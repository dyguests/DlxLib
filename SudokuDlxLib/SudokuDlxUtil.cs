using System.Collections.Generic;
using System.Linq;
using SudokuLib;

namespace SudokuDlxLib
{
    public static class SudokuDlxUtil
    {
        public static int[,] ToMatrix(IPuzzle puzzle)
        {
            var positionRows = CreatePositionRows(puzzle.Digits.Length);
            return RowsToMatrix(positionRows);
        }

        private static IEnumerable<int[]> CreatePositionRows(int size)
        {
            return Enumerable.Range(0, size).Select(i =>
            {
                var row = new int[size];
                row[i] = 1;
                return row;
            });
        }

        private static int[,] RowsToMatrix(IEnumerable<int[]> rows)
        {
            var rowList = rows.ToList();
            var matrix = new int[rowList.Count, rowList[0].Length];
            for (var i = 0; i < rowList.Count; i++)
            {
                for (var j = 0; j < rowList[i].Length; j++)
                {
                    matrix[i, j] = rowList[i][j];
                }
            }

            return matrix;
        }
    }
}
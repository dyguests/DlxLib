using System.Collections.Generic;
using System.Linq;

namespace SudokuDlxLib
{
    public static class DlxUtil
    {
        public static int[,] RowsToMatrix(this IEnumerable<int[]> rows)
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

        public static IEnumerable<int[]> MatrixToRows(this int[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                var row = new int[matrix.GetLength(1)];
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    row[j] = matrix[i, j];
                }
                yield return row;
            }
        }
    }
}
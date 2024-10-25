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

        /// <summary>
        /// 0b000_000_111 to [1, 2, 3]
        /// </summary>
        /// <param name="possibleDigitsBinary"></param>
        /// <returns></returns>
        public static IEnumerable<int> PossibleDigitsFromBinaryToEnumerable(this int possibleDigitsBinary)
        {
            for (var i = 0; i < 9; i++)
            {
                if ((possibleDigitsBinary & (0b1 << i)) == 0) continue;
                yield return i + 1;
            }
        }

        /// <summary>
        /// [1, 2, 3] to 0b000_000_111
        /// </summary>
        /// <param name="possibleDigits"></param>
        /// <returns></returns>
        public static int PossibleDigitsFromEnumerableToBinary(this IEnumerable<int> possibleDigits)
        {
            var possibleDigitsBinary = 0;
            foreach (var digit in possibleDigits)
            {
                possibleDigitsBinary |= 0b1 << (digit - 1);
            }

            return possibleDigitsBinary;
        }
    }
}
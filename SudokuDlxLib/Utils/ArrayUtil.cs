using System.Collections.Generic;
using System.Linq;

namespace SudokuDlxLib.Utils
{
    public static class ArrayUtil
    {
        public static int[,] To2DArray(IEnumerable<int[]> arrayIEnumerable)
        {
            var arrays = arrayIEnumerable.ToArray();
            if (arrays.Length == 0)
            {
                return new int[0, 0];
            }

            var array = new int[arrays.Length, arrays[0].Length];
            for (var i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = arrays[i][j];
                }
            }

            return array;
        }

        public static IEnumerable<int[]> ToArrayIEnumerable(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                var cols = matrix.GetLength(1);
                var array = new int[cols];
                for (int col = 0; col < cols; col++)
                {
                    array[col] = matrix[row, col];
                }

                yield return array;
            }
        }

        public static bool HashSameKeyColumns(int[] current, int[] other, int[] keyColumns)
        {
            foreach (var keyColumn in keyColumns)
            {
                if (keyColumn >= current.Length || keyColumn >= other.Length)
                {
                    return false;
                }

                if (current[keyColumn] != other[keyColumn])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
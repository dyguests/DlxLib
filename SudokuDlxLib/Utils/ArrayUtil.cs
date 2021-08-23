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
    }
}
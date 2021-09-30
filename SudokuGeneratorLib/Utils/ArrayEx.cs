using System;

namespace SudokuGeneratorLib.Utils
{
    public static class ArrayEx
    {
        private static readonly Random random = new Random();

        public static int[] Flatten(this int[,] input)
        {
            // Step 1: get total size of 2D array, and allocate 1D array.
            int size = input.Length;
            int[] result = new int[size];

            // Step 2: copy 2D array elements into a 1D array.
            int write = 0;
            for (int i = 0; i <= input.GetUpperBound(0); i++)
            {
                for (int z = 0; z <= input.GetUpperBound(1); z++)
                {
                    result[write++] = input[i, z];
                }
            }

            // Step 3: return the new array.
            return result;
        }

        /// <summary>
        /// 仅处理 正方形矩阵
        /// </summary>
        /// <param name="arr"></param>
        /// <typeparam name="T"></typeparam>
        public static void Transpose<T>(this T[,] arr)
        {
            int rowCount = arr.GetLength(0);
            // int columnCount = arr.GetLength(1);
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = i; j < rowCount; j++)
                {
                    T temp = arr[i, j];
                    arr[i, j] = arr[j, i];
                    arr[j, i] = temp;
                }
            }
        }

        public static T[,] TransposeNew<T>(this T[,] arr)
        {
            int rowCount = arr.GetLength(0);
            int columnCount = arr.GetLength(1);
            T[,] transposed = new T[columnCount, rowCount];
            if (rowCount == columnCount)
            {
                transposed = (T[,]) arr.Clone();
                for (int i = 1; i < rowCount; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        T temp = transposed[i, j];
                        transposed[i, j] = transposed[j, i];
                        transposed[j, i] = temp;
                    }
                }
            }
            else
            {
                for (int column = 0; column < columnCount; column++)
                {
                    for (int row = 0; row < rowCount; row++)
                    {
                        transposed[column, row] = arr[row, column];
                    }
                }
            }

            return transposed;
        }

        /// <summary>
        /// 洗混矩阵
        ///
        /// 注意：只是各行相互洗混
        /// 行内元素顺序不变
        ///
        /// 当使用dlx求解matrix时，若有多个解，本方法洗混功能可以使解随机
        /// </summary>
        /// <param name="arr"></param>
        /// <typeparam name="T"></typeparam>
        public static void ShuffleDimension0<T>(this T[,] arr)
        {
            var upperBound0 = arr.GetUpperBound(0);
            for (int i = 0; i <= upperBound0; i++)
            {
                var i2 = random.Next(upperBound0);
                for (int z = 0; z <= arr.GetUpperBound(1); z++)
                {
                    var tmp = arr[i, z];
                    arr[i, z] = arr[i2, z];
                    arr[i2, z] = tmp;
                }
            }
        }
    }
}
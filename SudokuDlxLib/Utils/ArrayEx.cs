using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuDlxLib.Utils
{
    public static class ArrayEx
    {
        public static T[] GetColumn<T>(this T[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
        }

        public static T[] GetRow<T>(this T[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (action == null) throw new ArgumentNullException("action");

            foreach (T item in source)
            {
                action(item);
            }
        }

        public static T[] SubArray<T>(this T[] array, int offset, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, offset, result, 0, length);
            return result;
        }

        public static int[] Add(this int[] array, int item)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.GetUpperBound(0)] = item;
            return array;
        }

        public static int[] AddUnique(this int[] array, int item)
        {
            if (!array.Contains(item))
            {
                Array.Resize(ref array, array.Length + 1);
                array[array.GetUpperBound(0)] = item;
            }

            return array;
        }
    }
}
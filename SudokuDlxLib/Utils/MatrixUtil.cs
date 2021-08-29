using System.Collections.Generic;
using System.Linq;

namespace SudokuDlxLib.Utils
{
    public static class MatrixUtil
    {
        private const int TileCount = 9 * 9;
        private const int NumberCount = 9;

        private static readonly int[] KeyColumns = new int[TileCount + NumberCount];

        static MatrixUtil()
        {
            for (var i = 0; i < KeyColumns.Length; i++)
            {
                KeyColumns[i] = i;
            }
        }

        public static void Expand(this Matrix current, Matrix other)
        {
            Expand(current, other, KeyColumns);
        }

        public static void Expand(this Matrix current, Matrix other, int[] keyColumns)
        {
            if (current.matrix == null)
            {
                current.matrix = other.matrix;
                current.primaryColumns = other.primaryColumns;
                current.secondaryColumns = other.secondaryColumns;
                return;
            }

            var currLength = current.matrix.GetLength(1);
            var otherLength = other.matrix.GetLength(1);
            var newLength = (keyColumns.Length) + (currLength - keyColumns.Length) + (otherLength - keyColumns.Length);

            var currRows = ArrayUtil.ToArrayIEnumerable(current.matrix).ToArray();
            var otherRows = ArrayUtil.ToArrayIEnumerable(other.matrix).ToArray();

            var result = new List<int[]>();

            foreach (var currRow in currRows)
            {
                var row = new int[newLength];
                foreach (var keyColumn in keyColumns)
                {
                    row[keyColumn] = currRow[keyColumn];
                }

                for (var col = 0; col < currRow.Length; col++)
                {
                    if (keyColumns.Contains(col)) continue;
                    row[col] = currRow[col];
                }


                var otherMatchRows = otherRows.Where(_row => ArrayUtil.HashSameKeyColumns(currRow, _row, keyColumns)).ToArray();
                if (otherMatchRows.Length == 0)
                {
                    result.Add(row);
                }
                else
                {
                    foreach (var otherMatchRow in otherMatchRows)
                    {
                        var cloneRow = row.Clone() as int[];
                        for (var col = 0; col < otherMatchRow.Length; col++)
                        {
                            if (keyColumns.Contains(col)) continue;
                            // todo 下面用keyColumns.Length 只在 keyColumns都在最左侧时才生效 
                            cloneRow[currLength + (col - keyColumns.Length)] = otherMatchRow[col];
                        }

                        result.Add(cloneRow);
                    }
                }
            }

            foreach (var otherRow in otherRows)
            {
                var currMatchRows = currRows.Where(_row => ArrayUtil.HashSameKeyColumns(otherRow, _row, keyColumns));
                if (currMatchRows.Any()) continue;

                var row = new int[newLength];
                foreach (var keyColumn in keyColumns)
                {
                    row[keyColumn] = otherRow[keyColumn];
                }

                for (var col = 0; col < otherRow.Length; col++)
                {
                    if (keyColumns.Contains(col)) continue;
                    row[currLength + (col - keyColumns.Length)] = otherRow[col];
                }

                result.Add(row);
            }

            current.matrix = ArrayUtil.To2DArray(result);
            if (current.primaryColumns != null && other.primaryColumns != null)
            {
                current.primaryColumns = current.primaryColumns.Union(other.primaryColumns.Select(index => currLength + index - keyColumns.Length)).ToArray();
            }
            else if (other.primaryColumns != null)
            {
                current.primaryColumns = other.primaryColumns.Select(index => currLength + index - keyColumns.Length).ToArray();
            }

            if (current.secondaryColumns != null && other.secondaryColumns != null)
            {
                current.secondaryColumns = current.secondaryColumns.Union(other.secondaryColumns.Select(index => currLength + index - keyColumns.Length)).ToArray();
            }
            else if (other.secondaryColumns != null)
            {
                current.secondaryColumns = other.secondaryColumns.Select(index => currLength + index - keyColumns.Length).ToArray();
            }
        }

        public static string MatrixToString(this int[,] matrix)
        {
            return string.Join(
                "\n",
                matrix.OfType<int>()
                    .Select((value, index) => new {value, index})
                    .GroupBy(x => x.index / matrix.GetLength(1))
                    .Select(x => string.Join("", x.Select(y => y.value)))
                    .Select((s, i) => $"{i,4:0000}:{s}")
            );
        }

        public static string ArrayToString(this int[] array)
        {
            return string.Join(",", array);
        }
    }
}
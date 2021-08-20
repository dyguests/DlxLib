using System;

namespace DlxLib
{
    /// <summary>
    /// Dancing links
    /// </summary>
    public class Dlx
    {
        public static int[][] Solve(int[,] matrix)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            return new int[0][];
        }
    }

    public class DataObject
    {
        public DataObject l, r, u, d, c;
    }

    public class ColumnObject : DataObject
    {
        public int s;

        /// <summary>
        /// row index
        /// </summary>
        public int n;
    }
}
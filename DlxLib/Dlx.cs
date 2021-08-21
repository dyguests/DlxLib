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
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));

            var h = BuildSparseMatrix(matrix);
            return Solve(h);
        }

        private static int[][] Solve(ColumnObject h)
        {
            throw new NotImplementedException();
        }

        private static ColumnObject BuildSparseMatrix(int[,] matrix)
        {
            var h = new ColumnObject();
            for (var row = -1; row < matrix.GetLength(0); row++)
            {
                DataObject c = h;
                DataObject d = null;
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    if (row == -1)
                    {
                        c.LinkRight(new ColumnObject());
                        c = c.r;
                    }
                    else
                    {
                        c = (ColumnObject) c.r;
                        if (matrix[col, row] == 1)
                        {
                            c.u.LinkDown(new DataObject());
                            // *1.将当列最下面的元素先暂存到c.u中(在跳舞前c.u还用不上),之后再还原。
                            c.u = c.u.d;
                            d?.LinkRight(c.u);
                            d = c.u;
                        }
                    }

                    if (row == matrix.GetLength(0) - 1)
                    {
                        // 这里对*1进行还原
                        c.u = c;
                    }
                }
            }

            return h;
        }
    }

    public class DataObject
    {
        public DataObject l, r, u, d;
        public ColumnObject c;

        public DataObject()
        {
            l = r = u = d = this;
        }

        public void LinkRight(DataObject another)
        {
            this.r = another;
            another.l = this;
        }

        public void LinkDown(DataObject another)
        {
            this.d = another;
            another.u = this;
        }
    }

    public class ColumnObject : DataObject
    {
        public int s;

        /// <summary>
        /// row index
        /// </summary>
        public int n;

        public ColumnObject()
        {
            c = this;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

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
            var O = new List<DataObject>();
            return Search(0, h, O);
        }

        /// <summary>
        /// Our nondeterministic algorithm to find all exact covers can now be cast in the following explicit, deterministic form as a recursive procedure search(k), which is invoked initially with k = 0
        /// </summary>
        /// <param name="k"></param>
        /// <param name="h"></param>
        /// <param name="O"></param>
        /// <returns></returns>
        private static int[][] Search(int k, ColumnObject h, List<DataObject> O)
        {
            // If R[h] = h, print the current solution (see below) and return.
            if (h.R == h)
            {
                Console.WriteLine("Solution:" + string.Join(",", O.Select(dataObject => dataObject.Row))); // todo
                return null;
            }

            // Otherwise choose a column object c (see below).
            var s = int.MaxValue;
            var jj = h;
            var c = jj;
            while (jj != jj.R)
            {
                jj = jj.R as ColumnObject;

                if (jj.S < s)
                {
                    c = jj;
                    s = jj.S;
                }
            }

            // Cover column c (see below).
            CoverColumnC(c);


            DataObject r = c;
            while (r != r.D)
            {
                r = r.D; // 这行可以合到while中去？

                O[k] = r;

                var j = r;
                while (j != r.R)
                {
                    // todo cover j
                }

                Search(k + 1, h, O);

                r = O[k];
                c = r.C;

                j = r;
                while (j != r.L)
                {
                    j = j.L;
                    // todo un cover j
                }
            }

            // todo un cover c
            return null;
        }

        private static ColumnObject BuildSparseMatrix(int[,] matrix)
        {
            var h = new ColumnObject();
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                var c = h;
                DataObject r = null;
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    if (row == 0)
                    {
                        h.AppendToRow(new ColumnObject());
                    }

                    c = c.R as ColumnObject;
                    if (matrix[col, row] == 1)
                    {
                        c.AppendToCol(new DataObject(c, row));
                        r?.AppendToRow(c.U);
                        r = c.U;
                    }
                }
            }

            return h;
        }

        private static void CoverColumnC(ColumnObject c)
        {
            c.R.L = c.L;
            c.L.R = c.R;

            DataObject i = c;
            while (i != i.D)
            {
                i = i.D;

                var j = i;
                while (j != j.R)
                {
                    j = i.R;

                    j.D.U = j.U;
                    j.U.D = j.D;
                    j.C.S--;
                }
            }
        }
    }

    public class DataObject
    {
        public DataObject L { get; set; }
        public DataObject R { get; set; }
        public DataObject U { get; set; }
        public DataObject D { get; set; }
        public ColumnObject C { get; set; }

        /// <summary>
        /// 所在行
        /// </summary>
        public int Row { get; }

        protected DataObject()
        {
            L = R = U = D = this;
        }

        public DataObject(ColumnObject c, int row) : this()
        {
            this.C = c;
            this.Row = row;
        }

        /// <summary>
        /// 在行尾添加dataObject
        /// </summary>
        /// <param name="dataObject"></param>
        public void AppendToRow(DataObject dataObject)
        {
            L.R = dataObject;
            dataObject.R = this;
            dataObject.L = L;
            L = dataObject;
        }
    }

    public class ColumnObject : DataObject
    {
        /// <summary>
        /// size
        /// </summary>
        public int S { get; set; }

        // /// <summary>
        // /// name
        // /// </summary>
        // public int n;

        public ColumnObject()
        {
            C = this;
        }

        /// <summary>
        /// 在列尾添加dataObject
        /// </summary>
        /// <param name="dataObject"></param>
        public void AppendToCol(DataObject dataObject)
        {
            U.D = dataObject;
            dataObject.D = this;
            dataObject.U = U;
            U = dataObject;

            S++;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace DlxLib
{
    /// <summary>
    /// Dancing links
    /// </summary>
    public static class Dlx
    {
        public static int[][] Solve(int[,] matrix)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));

            var h = BuildSparseMatrix(matrix);
            // Print(h);
            var o = new Dictionary<int, DataObject>();
            return Search(0, h, o);
        }

        // /// <summary>
        // /// todo deprecated
        // /// </summary>
        // /// <param name="h"></param>
        // private static void Print(ColumnObject h)
        // {
        //     var c = h.R;
        //     while (c != h)
        //     {
        //         var r = c.D;
        //         while (r != c)
        //         {
        //             Console.Write(r.TmpName + "  ");
        //             r = r.D;
        //         }
        //
        //         Console.WriteLine();
        //
        //         c = c.R;
        //     }
        // }

        /// <summary>
        /// Our nondeterministic algorithm to find all exact covers can now be cast in the following explicit, deterministic form as a recursive procedure search(k), which is invoked initially with k = 0
        /// </summary>
        /// <param name="k"></param>
        /// <param name="h"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        private static int[][] Search(int k, ColumnObject h, Dictionary<int, DataObject> o)
        {
            // If R[h] = h, print the current solution (see below) and return.
            if (h.R == h)
            {
                Console.WriteLine("Solution:" + string.Join(",", o.OrderBy(pair => pair.Key).Select(pair => pair.Value).Select(dataObject => dataObject.Row))); // todo
                return null;
            }

            // Otherwise choose a column object c (see below).
            var c = ChooseColumnC(h);

            // Cover column c (see below).
            CoverColumnC(c);

            // For each r ← D[c], D[D[c]], . . . , while r = c,
            for (var r = c.D; r != c; r = r.D)
            {
                // set Ok ← r;
                o[k] = r;

                // for each j ← R[r], RR[r], . . . , while j = r,
                for (var j = r.R; j != r; j = j.R)
                {
                    // cover column j (see below);
                    CoverColumnC(j.C);
                }

                // search(k + 1);
                Search(k + 1, h, o);

                // set r ← Ok and c ← C[r];
                r = o[k];
                c = r.C;

                // for each j ← L[r], L[L[r]], . . . , while j = r,
                for (var j = r.L; j != r; j = j.L)
                {
                    // uncover column j (see below).
                    UncoverColumnC(j.C);
                }
            }

            // Uncover column c (see below)
            UncoverColumnC(c);
            return null;
        }

        /// <summary>
        /// 注意稀疏矩阵是一个十字环形链表。（“十字”、“环形”）
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>list header</returns>
        private static ColumnObject BuildSparseMatrix(int[,] matrix)
        {
            var h = new ColumnObject(-1);
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                var c = h;
                DataObject r = null;
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    if (row == 0)
                    {
                        h.AppendToRow(new ColumnObject(col));
                    }

                    c = c.R as ColumnObject;
                    if (matrix[row, col] == 1)
                    {
                        c.AppendToCol(new DataObject(c, row));
                        r?.AppendToRow(c.U);
                        r = c.U;
                    }
                }
            }

            return h;
        }

        private static ColumnObject ChooseColumnC(ColumnObject h)
        {
            // return h.R as ColumnObject;

            var s = int.MaxValue;
            var j = h.R as ColumnObject;
            var c = j;
            while (j != h)
            {
                if (j.S < s)
                {
                    c = j;
                    s = j.S;
                }

                j = j.R as ColumnObject;
            }

            return c;
        }

        private static void CoverColumnC(ColumnObject c)
        {
            c.R.L = c.L;
            c.L.R = c.R;

            for (var i = c.D; i != c; i = i.D)
            {
                for (var j = i.R; j != i; j = j.R)
                {
                    j.D.U = j.U;
                    j.U.D = j.D;
                    j.C.S--;
                }
            }
        }

        private static void UncoverColumnC(ColumnObject c)
        {
            for (var i = c.U; i != c; i = i.U)
            {
                for (var j = i.L; j != i; j = i.L)
                {
                    j.C.S++;
                    j.D.U = j;
                    j.U.D = j;
                }
            }

            c.R.L = c;
            c.L.R = c;
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

        public override string ToString()
        {
            return "(" + C.N + "," + Row + ")";
        }
    }

    public class ColumnObject : DataObject
    {
        /// <summary>
        /// size
        /// </summary>
        public int S { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public int N { get; set; }

        public ColumnObject(int name)
        {
            C = this;

            N = name;
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

        public override string ToString()
        {
            return "(" + N + ",C)";
        }
    }
}
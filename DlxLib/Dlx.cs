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
        public static IEnumerable<int[]> Solve(int[,] matrix, int numPrimaryColumns = int.MaxValue)
        {
            return Solve(matrix, numPrimaryColumns, new UpToTwoInstrumentation());
        }

        public static IEnumerable<int[]> Solve(int[,] matrix, int numPrimaryColumns, params Instrumentation[] instrumentations)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));

            var h = BuildSparseMatrix(matrix, numPrimaryColumns);
            var o = new Dictionary<int, DataObject>();
            return Search(0, h, o, instrumentations);
        }

        /// <summary>
        /// 注意稀疏矩阵是一个十字环形链表。（“十字”、“环形”）
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numPrimaryColumns"></param>
        /// <returns>list header</returns>
        private static ColumnObject BuildSparseMatrix(int[,] matrix, int numPrimaryColumns)
        {
            var h = new ColumnObject(-1);
            var listHeaders = new Dictionary<int, ColumnObject>();
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                // var c = h;
                DataObject r = null;
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    if (row == 0)
                    {
                        var listHeader = new ColumnObject(col);
                        if (col < numPrimaryColumns) h.AppendToRow(listHeader);
                        listHeaders[col] = listHeader;
                    }

                    var c = listHeaders[col];
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

        /// <summary>
        /// Our nondeterministic algorithm to find all exact covers can now be cast in the following explicit, deterministic form as a recursive procedure search(k), which is invoked initially with k = 0
        /// </summary>
        /// <param name="k"></param>
        /// <param name="h"></param>
        /// <param name="o"></param>
        /// <param name="instrumentations"></param>
        /// <returns></returns>
        private static IEnumerable<int[]> Search(int k, ColumnObject h, Dictionary<int, DataObject> o, Instrumentation[] instrumentations)
        {
            if (instrumentations?.Any(instrumentation => instrumentation.IsCancelled()) == true)
            {
                yield break;
            }

            // If R[h] = h, print the current solution (see below) and return.
            if (h.R == h)
            {
                foreach (var instrumentation in instrumentations)
                {
                    instrumentation.NotifySolutionIncrease();
                }

                // Console.WriteLine("Solution:" + string.Join(",", o.OrderBy(pair => pair.Key).Select(pair => pair.Value).Select(dataObject => dataObject.Row))); // todo
                yield return o.OrderBy(pair => pair.Key).Select(pair => pair.Value).Select(dataObject => dataObject.Row).ToArray();
                yield break;
            }

            // Otherwise choose a column object c (see below).
            var c = ChooseColumnC(h);

            try
            {
                // Cover column c (see below).
                CoverColumnC(c);

                // For each r ← D[c], D[D[c]], . . . , while r = c,
                for (var r = c.D; r != c; r = r.D)
                {
                    if (instrumentations?.Any(instrumentation => instrumentation.IsCancelled()) == true)
                    {
                        yield break;
                    }

                    // set Ok ← r;
                    o[k] = r;

                    // for each j ← R[r], RR[r], . . . , while j = r,
                    for (var j = r.R; j != r; j = j.R)
                    {
                        // cover column j (see below);
                        CoverColumnC(j.C);
                    }

                    // search(k + 1);
                    var solutions = Search(k + 1, h, o, instrumentations);
                    foreach (var solution in solutions)
                    {
                        yield return solution;
                    }

                    // set r ← Ok and c ← C[r];
                    // r = o[k];
                    o.Remove(k);
                    // c = r.C;

                    // for each j ← L[r], L[L[r]], . . . , while j = r,
                    for (var j = r.L; j != r; j = j.L)
                    {
                        // uncover column j (see below).
                        UncoverColumnC(j.C);
                    }
                }
            }
            finally
            {
                // Uncover column c (see below)
                UncoverColumnC(c);
            }
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
                for (var j = i.L; j != i; j = j.L)
                {
                    j.C.S++;
                    j.D.U = j;
                    j.U.D = j;
                }
            }

            c.R.L = c;
            c.L.R = c;
        }

        /// <summary>
        /// 插桩逻辑
        ///
        /// 如手动取消、超过两个结果不再检查
        /// </summary>
        public abstract class Instrumentation
        {
            private bool isCancelled;

            public void Cancel()
            {
                isCancelled = true;
            }

            public bool IsCancelled()
            {
                return isCancelled;
            }

            public virtual void NotifySolutionIncrease()
            {
            }
        }

        public class DefaultInstrumentation : Instrumentation
        {
        }

        /// <summary>
        /// 对于求解是否具有唯一解，只取得两个解的情况下，后续就不会再求解也可以确定不具有唯一解了
        /// </summary>
        public class UpToTwoInstrumentation : Instrumentation
        {
            private int numberOfSolutions;

            public override void NotifySolutionIncrease()
            {
                base.NotifySolutionIncrease();
                numberOfSolutions++;
                if (numberOfSolutions >= 2)
                {
                    Cancel();
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
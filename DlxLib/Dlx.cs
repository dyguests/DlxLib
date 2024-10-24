using System;
using System.Collections.Generic;
using System.Linq;
using DlxLib.ColumnPredicates;
using DlxLib.Instrumentations;

namespace DlxLib
{
    /// <summary>
    /// Dancing links
    /// </summary>
    public static class Dlx
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="numPrimaryColumns">It means 0~numPrimaryColumns are primary columns</param>
        /// <returns></returns>
        public static IEnumerable<int[]> Solve(int[,] matrix, int numPrimaryColumns = int.MaxValue)
        {
            return Solve(matrix, new NumPrimaryColumnsPredicate(numPrimaryColumns));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="secondaryColumns">It means any col not in secondaryColumns is primary columns</param>
        /// <returns></returns>
        public static IEnumerable<int[]> Solve(int[,] matrix, int[] secondaryColumns)
        {
            return Solve(matrix, new SecondaryColumnsPredicate(secondaryColumns));
        }

        /// <summary>
        /// 一般用于除了主列、副列还有提示列的情况
        /// 提示列：不参与DLX算法，参与matrix join。
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="primaryColumns">主列</param>
        /// <param name="secondaryColumns">副列</param>
        /// <returns></returns>
        public static IEnumerable<int[]> Solve(int[,] matrix, int[] primaryColumns, int[] secondaryColumns)
        {
            return Solve(matrix, new ColumnsPredicate(primaryColumns, secondaryColumns));
        }

        public static IEnumerable<int[]> Solve(int[,] matrix, int[] primaryColumns, int[] secondaryColumns, params Instrumentation[] instrumentations)
        {
            return Solve(matrix, new ColumnsPredicate(primaryColumns, secondaryColumns), instrumentations);
        }

        public static IEnumerable<int[]> Solve(int[,] matrix, IColumnPredicate columnPredicate)
        {
            return Solve(matrix, columnPredicate, new UpToTwoInstrumentation());
        }

        public static IEnumerable<int[]> Solve(int[,] matrix, IColumnPredicate columnPredicate, params Instrumentation[] instrumentations)
        {
            if (matrix == null) throw new ArgumentNullException(nameof(matrix));

            var h = BuildSparseMatrix(matrix, columnPredicate);
            var o = new Stack<DataObject>();
            return Search(0, h, o, instrumentations);
        }

        /// <summary>
        /// 注意稀疏矩阵是一个十字环形链表。（“十字”、“环形”）
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="columnPredicate"></param>
        /// <returns>list header</returns>
        private static ColumnObject BuildSparseMatrix(int[,] matrix, IColumnPredicate columnPredicate)
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
                        if (columnPredicate.IsPrimaryColumn(col))
                        {
                            h.AppendToRow(listHeader);
                        }

                        if (columnPredicate.IsPrimaryColumn(col) || columnPredicate.IsSecondaryColumn(col))
                        {
                            listHeaders[col] = listHeader;
                        }
                    }

                    if (columnPredicate.IsPrimaryColumn(col) || columnPredicate.IsSecondaryColumn(col))
                    {
                        var c = listHeaders[col];
                        if (matrix[row, col] == 1)
                        {
                            c.AppendToCol(new DataObject(c, row));
                            r?.AppendToRow(c.U);
                            r = c.U;
                        }
                    }
                }
            }

            return h;
        }

        /// <summary>
        /// Our nondeterministic algorithm to find all exact covers can now be cast in the following explicit, deterministic form as a recursive procedure search(k), which is invoked initially with k = 0
        /// </summary>
        /// <param name="k">递归层次</param>
        /// <param name="h"></param>
        /// <param name="o"></param>
        /// <param name="instrumentations"></param>
        /// <returns></returns>
        private static IEnumerable<int[]> Search(int k, ColumnObject h, Stack<DataObject> o, Instrumentation[] instrumentations)
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

                yield return o.Select(dataObject => dataObject.Row).Reverse().ToArray();
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
                    o.Push(r);

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
                    o.Pop();

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
    }
}
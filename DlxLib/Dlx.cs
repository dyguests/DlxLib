using System;
using System.Collections.Generic;
using System.Linq;
using DlxLib.Beans;
using DlxLib.ColumnPredicates;
using DlxLib.Instrumentations;

namespace DlxLib
{
    /// <summary>
    /// Dancing links
    /// </summary>
    public class Dlx
    {
        private readonly int[,] _matrix;
        public int[,] ReadonlyMatrix => (int[,])_matrix.Clone();
        private readonly IColumnPredicate _columnPredicate;
        private readonly Instrumentation[] _instrumentations;

        public Dlx(int[,] matrix) : this(matrix, matrix.GetLength(0)) { }

        public Dlx(int[,] matrix, int numPrimaryColumns) : this(matrix, new NumPrimaryColumnsPredicate(numPrimaryColumns)) { }

        public Dlx(int[,] matrix, int[] primaryColumnIndexes, int[] secondaryColumnIndexes) :
            this(matrix, new NormalColumnsPredicate(primaryColumnIndexes, secondaryColumnIndexes), new UpToTwoInstrumentation())
        {
            if (primaryColumnIndexes == null) throw new ArgumentNullException(nameof(primaryColumnIndexes));
            if (secondaryColumnIndexes == null) throw new ArgumentNullException(nameof(secondaryColumnIndexes));
        }

        public Dlx(int[,] matrix, int[] columnPredicate) : this(matrix, new IndexColumnsPredicate(columnPredicate), new UpToTwoInstrumentation())
        {
            if (columnPredicate == null) throw new ArgumentNullException(nameof(columnPredicate));
        }

        public Dlx(int[,] matrix, IColumnPredicate columnPredicate, params Instrumentation[] instrumentations)
        {
            this._matrix = matrix ?? throw new ArgumentNullException(nameof(matrix));
            this._columnPredicate = columnPredicate ?? throw new ArgumentNullException(nameof(columnPredicate));
            this._instrumentations = instrumentations ?? throw new ArgumentNullException(nameof(instrumentations));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>IEnumerable.each 是 rowIndexes</returns>
        public IEnumerable<Solution> Solve()
        {
            var header = BuildSparseMatrix(_matrix, _columnPredicate);
            var o = new Stack<Node>();

            return Search(header, o, _instrumentations, 0, new[] { 0 });
        }

        /// <summary>
        /// 注意稀疏矩阵是一个十字环形链表。（“十字”、“环形”）
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="columnPredicate"></param>
        /// <returns>list header</returns>
        private static Column BuildSparseMatrix(int[,] matrix, IColumnPredicate columnPredicate)
        {
            var header = new Column(-1);
            var listHeaders = new Dictionary<int, Column>();
            for (var row = 0; row < matrix.GetLength(0); row++)
            {
                // var column = header;
                Node preview = null;
                for (var col = 0; col < matrix.GetLength(1); col++)
                {
                    if (row == 0)
                    {
                        var listHeader = new Column(col, columnPredicate.IsPrimaryColumn(col));
                        if (columnPredicate.IsPrimaryColumn(col) || columnPredicate.IsSecondaryColumn(col))
                        {
                            header.AppendToRow(listHeader);
                        }

                        if (columnPredicate.IsPrimaryColumn(col) || columnPredicate.IsSecondaryColumn(col))
                        {
                            listHeaders[col] = listHeader;
                        }
                    }

                    if (columnPredicate.IsPrimaryColumn(col) || columnPredicate.IsSecondaryColumn(col))
                    {
                        var column = listHeaders[col];
                        if (matrix[row, col] == 1)
                        {
                            column.AppendToCol(new Node(column, row));
                            preview?.AppendToRow(column.U);
                            preview = column.U;
                        }
                    }
                }
            }

            return header;
        }

        /// <summary>
        /// Our nondeterministic algorithm to find all exact covers can now be cast in the following explicit, deterministic form as a recursive procedure search(k), which is invoked initially with k = 0
        /// </summary>
        /// <param name="header"></param>
        /// <param name="stack"></param>
        /// <param name="instrumentations"></param>
        /// <param name="deep">递归层次</param>
        /// <param name="step">计算次数 new int[1]</param>
        /// <returns></returns>
        private static IEnumerable<Solution> Search(
            Column header,
            Stack<Node> stack,
            Instrumentation[] instrumentations,
            int deep,
            int[] step
        )
        {
            foreach (var instrumentation in instrumentations)
            {
                instrumentation.OnSearchStart(stack);
            }

            if (instrumentations.Any(instrumentation => instrumentation.ShouldInterrupt()))
            {
                yield break;
            }


            // If R[h] = h, print the current solution (see below) and return.
            // if (header.R == header)
            if (header.IsUniquePrimaryColumn())
            {
                foreach (var instrumentation in instrumentations)
                {
                    instrumentation.OnSolutionFound();
                }

                yield return new Solution
                {
                    RowIndexes = stack.Select(node => node.Row).ToArray(),
                    Deep = deep,
                    Step = step[0],
                };
                yield break;
            }

            // Otherwise choose a column object c (see below).
            var c = ChooseColumnC(header);

            try
            {
                // Cover column c (see below).
                CoverColumnC(c);

                // For each r ← D[c], D[D[c]], . . . , while r = c,
                for (var r = c.D; r != c; r = r.D)
                {
                    if (instrumentations.Any(instrumentation => instrumentation.ShouldInterrupt()))
                    {
                        yield break;
                    }

                    // set Ok ← r;
                    stack.Push(r);

                    // for each j ← R[r], RR[r], . . . , while j = r,
                    for (var j = r.R; j != r; j = j.R)
                    {
                        // cover column j (see below);
                        CoverColumnC(j.C);
                    }

                    // search(k + 1);
                    step[0]++;
                    var solutions = Search(header, stack, instrumentations, deep + 1, step);
                    foreach (var solution in solutions)
                    {
                        yield return solution;
                    }

                    // set r ← Ok and c ← C[r];
                    stack.Pop();

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

        private static Column ChooseColumnC(Column h)
        {
            // return h.R as ColumnObject;

            var s = int.MaxValue;
            var j = h.R as Column;
            var c = j;
            while (j != h)
            {
                if (j.S < s)
                {
                    c = j;
                    s = j.S;
                }

                j = j.R as Column;
            }

            return c;
        }

        private static void CoverColumnC(Column c)
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

        private static void UncoverColumnC(Column c)
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

        public void Display()
        {
            Console.WriteLine("Dlx:");
            for (var i = 0; i < _matrix.GetLength(0); i++)
            {
                if (i == 0)
                {
                    Console.Write("C:");
                    for (var j = 0; j < _matrix.GetLength(1); j++)
                    {
                        if (_columnPredicate.IsPrimaryColumn(j))
                        {
                            Console.Write("0" + ",");
                        }
                        else if (_columnPredicate.IsSecondaryColumn(j))
                        {
                            Console.Write("1" + ",");
                        }
                        else
                        {
                            Console.Write("?" + ",");
                        }
                    }

                    Console.WriteLine();
                }

                Console.Write($"{i}:");
                for (var j = 0; j < _matrix.GetLength(1); j++)
                {
                    Console.Write(_matrix[i, j] + ",");
                }

                Console.WriteLine();
            }
        }
    }
}
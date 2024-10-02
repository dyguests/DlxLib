using System;
using System.Collections.Generic;

namespace DlxLib
{
    public class DlxSolver
    {
        private DlxColumn header;
        private List<DlxNode> solution;

        public DlxSolver(int[,] matrix, List<string> columnNames)
        {
            header = new DlxColumn("header");
            solution = new List<DlxNode>();
            DlxColumn[] columns = new DlxColumn[matrix.GetLength(1)];

            // 创建列
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                columns[i] = new DlxColumn(columnNames[i]);
                header.LinkRight(columns[i]);
            }

            // 创建节点
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                DlxNode firstNode = null;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        DlxNode newNode = new DlxNode { Column = columns[j] };
                        columns[j].LinkDown(newNode);
                        if (firstNode == null)
                        {
                            firstNode = newNode;
                        }
                        else
                        {
                            firstNode.LinkRight(newNode);
                        }
                        columns[j].Size++;
                    }
                }
            }
        }

        public void Search(int k)
        {
            if (header.Right == header)
            {
                // 找到解决方案
                PrintSolution();
                return;
            }

            // 选择列
            DlxColumn column = SelectColumn();
            column.Cover();

            for (DlxNode row = column.Down; row != column; row = row.Down)
            {
                solution.Add(row);

                for (DlxNode node = row.Right; node != row; node = node.Right)
                {
                    node.Column.Cover();
                }

                Search(k + 1);

                row = solution[solution.Count - 1];
                solution.RemoveAt(solution.Count - 1);
                column = row.Column;

                for (DlxNode node = row.Left; node != row; node = node.Left)
                {
                    node.Column.Uncover();
                }
            }
            column.Uncover();
        }

        private DlxColumn SelectColumn()
        {
            DlxColumn best = null;
            int minSize = int.MaxValue;
            for (DlxColumn column = (DlxColumn)header.Right; column != header; column = (DlxColumn)column.Right)
            {
                if (column.Size < minSize)
                {
                    best = column;
                    minSize = column.Size;
                }
            }
            return best;
        }

        private void PrintSolution()
        {
            foreach (var row in solution)
            {
                DlxNode node = row;
                do
                {
                    Console.Write(node.Column.Name + " ");
                    node = node.Right;
                } while (node != row);
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
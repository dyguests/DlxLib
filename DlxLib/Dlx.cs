using System;
using System.Collections.Generic;

namespace DlxLib
{
    public class Dlx
    {
        private Column header;
        private List<Node> solution;

        public Dlx(int[,] matrix, List<string>? columnNames = null)
        {
            header = new Column("header");
            solution = new List<Node>();
            Column[] columns = new Column[matrix.GetLength(1)];

            // 创建列
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                columns[i] = new Column(columnNames?[i] ?? $"column{i}");
                header.LinkRight(columns[i]);
            }

            // 创建节点
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Node firstNode = null;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        Node newNode = new Node
                        {
                            column = columns[j],
                            RowIndex = i,
                        };
                        columns[j].LinkDown(newNode);
                        if (firstNode == null)
                        {
                            firstNode = newNode;
                        }
                        else
                        {
                            firstNode.LinkRight(newNode);
                        }
                        columns[j].size++;
                    }
                }
            }
        }

        public void Search(int deep = 0)
        {
            if (header.right == header)
            {
                // 找到解决方案
                PrintSolution();
                return;
            }

            // 选择列
            Column column = SelectColumn();
            column.Cover();

            for (Node row = column.down; row != column; row = row.down)
            {
                solution.Add(row);

                for (Node node = row.right; node != row; node = node.right)
                {
                    node.column.Cover();
                }

                Search(deep + 1);

                row = solution[solution.Count - 1];
                solution.RemoveAt(solution.Count - 1);
                column = row.column;

                for (Node node = row.left; node != row; node = node.left)
                {
                    node.column.Uncover();
                }
            }
            column.Uncover();
        }

        private Column SelectColumn()
        {
            Column best = null;
            int minSize = int.MaxValue;
            for (Column column = (Column)header.right; column != header; column = (Column)column.right)
            {
                if (column.size < minSize)
                {
                    best = column;
                    minSize = column.size;
                }
            }
            return best;
        }

        private void PrintSolution()
        {
            foreach (var row in solution)
            {
                Console.Write($"Row {row.RowIndex}: "); // 输出行号
                Node node = row;
                do
                {
                    Console.Write(node.column.name + " ");
                    node = node.right;
                } while (node != row);
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
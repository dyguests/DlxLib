using System.Collections.Generic;
using System.Linq;

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
            var columns = new Column[matrix.GetLength(1)];

            // 创建列
            for (var i = 0; i < matrix.GetLength(1); i++)
            {
                columns[i] = new Column(columnNames?[i] ?? $"col{i}");
                header.LinkRight(columns[i]);
            }

            // 创建节点
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                Node firstNode = null;
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        var newNode = new Node
                        {
                            column = columns[j],
                            rowIndex = i,
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

        public IEnumerable<int[]> Search(int deep = 0)
        {
            if (header.right == header)
            {
                // 找到解决方案
                yield return solution.Select(node => node.rowIndex).ToArray();
                yield break;
            }

            // 选择列
            var column = SelectColumn();
            column.Cover();

            for (var row = column.down; row != column; row = row.down)
            {
                solution.Add(row);

                for (var node = row.right; node != row; node = node.right)
                {
                    node.column.Cover();
                }

                foreach (var result in Search(deep + 1))
                {
                    yield return result;
                }

                row = solution[^1];
                solution.RemoveAt(solution.Count - 1);
                column = row.column;

                for (var node = row.left; node != row; node = node.left)
                {
                    node.column.Uncover();
                }
            }
            column.Uncover();
        }

        private Column SelectColumn()
        {
            Column best = null;
            var minSize = int.MaxValue;
            for (var column = (Column)header.right; column != header; column = (Column)column.right)
            {
                if (column.size < minSize)
                {
                    best = column;
                    minSize = column.size;
                }
            }
            return best;
        }
    }
}
namespace DlxLib.Beans
{
    public class Node
    {
        public Node L { get; set; }
        public Node R { get; set; }
        public Node U { get; set; }
        public Node D { get; set; }
        public Column C { get; set; }

        /// <summary>
        /// 所在行
        /// </summary>
        public int Row { get; }

        protected Node()
        {
            L = R = U = D = this;
        }

        public Node(Column c, int row) : this()
        {
            this.C = c;
            this.Row = row;
        }

        /// <summary>
        /// 在行尾添加dataObject
        /// </summary>
        /// <param name="node"></param>
        public void AppendToRow(Node node)
        {
            L.R = node;
            node.R = this;
            node.L = L;
            L = node;
        }

        public override string ToString()
        {
            return "(" + C.N + "," + Row + ")";
        }
    }
}
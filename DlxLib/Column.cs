namespace DlxLib
{
    public class Column : Node
    {
        /// <summary>
        /// size
        /// </summary>
        public int S { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public int N { get; set; }

        public Column(int name)
        {
            C = this;

            N = name;
        }

        /// <summary>
        /// 在列尾添加dataObject
        /// </summary>
        /// <param name="node"></param>
        public void AppendToCol(Node node)
        {
            U.D = node;
            node.D = this;
            node.U = U;
            U = node;

            S++;
        }

        public override string ToString()
        {
            return "(" + N + ",C)";
        }
    }
}
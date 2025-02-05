namespace DlxLib.Beans
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

        /// <summary>
        /// 是否是主列
        ///
        /// 用于后续判断是否迭代完成。即当前header行只剩自身一个主列。副列可以忽略
        /// </summary>
        public bool IsPrimary { get; set; }

        public Column(int name, bool isPrimary = true)
        {
            C = this;

            N = name;
            IsPrimary = isPrimary;
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

        /// <summary>
        /// 判断是否是唯一的主列，若是，表明dlx完成
        /// </summary>
        /// <returns></returns>
        public bool IsUniquePrimaryColumn()
        {
            var right = (Column)R;
            while (right != this)
            {
                if (right.IsPrimary)
                {
                    return false;
                }

                right = (Column)right.R;
            }

            return true;
        }
    }
}
namespace DlxLib
{
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
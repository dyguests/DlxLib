namespace DlxLib
{
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
}
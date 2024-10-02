namespace DlxLib
{
    class Column : Node
    {
        public readonly string name;
        public int size;

        public Column(string name)
        {
            this.name = name;
            size = 0;
            column = this;
        }

        public void Cover()
        {
            UnlinkLeftRight();
            for (Node row = down; row != this; row = row.down)
            {
                for (Node node = row.right; node != row; node = node.right)
                {
                    node.UnlinkUpDown();
                    node.column.size--;
                }
            }
        }

        public void Uncover()
        {
            for (Node row = up; row != this; row = row.up)
            {
                for (Node node = row.left; node != row; node = node.left)
                {
                    node.RelinkUpDown();
                    node.column.size++;
                }
            }
            RelinkLeftRight();
        }
    }
}
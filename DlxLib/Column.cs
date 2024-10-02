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
            for (var row = down; row != this; row = row.down)
            {
                for (var node = row.right; node != row; node = node.right)
                {
                    node.UnlinkUpDown();
                    node.column.size--;
                }
            }
        }

        public void Uncover()
        {
            for (var row = up; row != this; row = row.up)
            {
                for (var node = row.left; node != row; node = node.left)
                {
                    node.RelinkUpDown();
                    node.column.size++;
                }
            }
            RelinkLeftRight();
        }
    }
}
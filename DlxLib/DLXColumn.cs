namespace DlxLib
{
    class DlxColumn : DlxNode
    {
        public int Size;
        public string Name;

        public DlxColumn(string name)
        {
            Size = 0;
            Name = name;
            Column = this;
        }

        public void Cover()
        {
            UnlinkLeftRight();
            for (DlxNode row = Down; row != this; row = row.Down)
            {
                for (DlxNode node = row.Right; node != row; node = node.Right)
                {
                    node.UnlinkUpDown();
                    node.Column.Size--;
                }
            }
        }

        public void Uncover()
        {
            for (DlxNode row = Up; row != this; row = row.Up)
            {
                for (DlxNode node = row.Left; node != row; node = node.Left)
                {
                    node.RelinkUpDown();
                    node.Column.Size++;
                }
            }
            RelinkLeftRight();
        }
    }
}
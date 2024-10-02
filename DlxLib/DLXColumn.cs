namespace DlxLib
{
    class DLXColumn : DLXNode
    {
        public int Size;
        public string Name;

        public DLXColumn(string name)
        {
            Size = 0;
            Name = name;
            Column = this;
        }

        public void Cover()
        {
            UnlinkLeftRight();
            for (DLXNode row = Down; row != this; row = row.Down)
            {
                for (DLXNode node = row.Right; node != row; node = node.Right)
                {
                    node.UnlinkUpDown();
                    node.Column.Size--;
                }
            }
        }

        public void Uncover()
        {
            for (DLXNode row = Up; row != this; row = row.Up)
            {
                for (DLXNode node = row.Left; node != row; node = node.Left)
                {
                    node.RelinkUpDown();
                    node.Column.Size++;
                }
            }
            RelinkLeftRight();
        }
    }
}
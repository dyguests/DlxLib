namespace DlxLib
{
    class Column : Node
    {
        public int Size;
        public string Name;

        public Column(string name)
        {
            Size = 0;
            Name = name;
            Column = this;
        }

        public void Cover()
        {
            UnlinkLeftRight();
            for (Node row = Down; row != this; row = row.Down)
            {
                for (Node node = row.Right; node != row; node = node.Right)
                {
                    node.UnlinkUpDown();
                    node.Column.Size--;
                }
            }
        }

        public void Uncover()
        {
            for (Node row = Up; row != this; row = row.Up)
            {
                for (Node node = row.Left; node != row; node = node.Left)
                {
                    node.RelinkUpDown();
                    node.Column.Size++;
                }
            }
            RelinkLeftRight();
        }
    }
}
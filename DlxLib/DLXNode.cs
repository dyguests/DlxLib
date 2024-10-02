namespace DlxLib
{
    class DlxNode
    {
        public DlxNode Left, Right, Up, Down;
        public DlxColumn Column;

        public DlxNode()
        {
            Left = Right = Up = Down = this;
        }

        public void LinkRight(DlxNode node)
        {
            node.Right = Right;
            node.Left = this;
            Right.Left = node;
            Right = node;
        }

        public void LinkDown(DlxNode node)
        {
            node.Down = Down;
            node.Up = this;
            Down.Up = node;
            Down = node;
        }

        public void UnlinkLeftRight()
        {
            Left.Right = Right;
            Right.Left = Left;
        }

        public void RelinkLeftRight()
        {
            Left.Right = this;
            Right.Left = this;
        }

        public void UnlinkUpDown()
        {
            Up.Down = Down;
            Down.Up = Up;
        }

        public void RelinkUpDown()
        {
            Up.Down = this;
            Down.Up = this;
        }
    }
}
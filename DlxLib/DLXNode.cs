namespace DlxLib
{
    class DLXNode
    {
        public DLXNode Left, Right, Up, Down;
        public DLXColumn Column;

        public DLXNode()
        {
            Left = Right = Up = Down = this;
        }

        public void LinkRight(DLXNode node)
        {
            node.Right = Right;
            node.Left = this;
            Right.Left = node;
            Right = node;
        }

        public void LinkDown(DLXNode node)
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
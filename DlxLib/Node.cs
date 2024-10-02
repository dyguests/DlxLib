namespace DlxLib
{
    class Node
    {
        public Node left, right, up, down;
        public Column column;

        public Node()
        {
            left = right = up = down = this;
        }

        public void LinkRight(Node node)
        {
            node.right = right;
            node.left = this;
            right.left = node;
            right = node;
        }

        public void LinkDown(Node node)
        {
            node.down = down;
            node.up = this;
            down.up = node;
            down = node;
        }

        public void UnlinkLeftRight()
        {
            left.right = right;
            right.left = left;
        }

        public void RelinkLeftRight()
        {
            left.right = this;
            right.left = this;
        }

        public void UnlinkUpDown()
        {
            up.down = down;
            down.up = up;
        }

        public void RelinkUpDown()
        {
            up.down = this;
            down.up = this;
        }
    }
}
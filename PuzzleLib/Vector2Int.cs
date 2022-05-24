namespace PuzzleLib
{
    public struct Vector2Int
    {
        public static readonly Vector2Int one = new Vector2Int(1, 1);

        public int x;
        public int y;

        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
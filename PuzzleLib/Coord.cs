namespace PuzzleLib.UnityEngine
{
    public struct Coord
    {
        public static readonly Coord one = new(1, 1);

        public int x;
        public int y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
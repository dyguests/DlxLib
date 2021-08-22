using System.Linq;

namespace UniqueTest
{
    public static class Util
    {
        public static string MatrixToString(this int[,] matrix)
        {
            return string.Join(
                "\n",
                matrix.OfType<int>()
                    .Select((value, index) => new {value, index})
                    .GroupBy(x => x.index / matrix.GetLength(1))
                    .Select(x => $"{{{string.Join(",", x.Select(y => y.value))}}}")
            );
        }
    }
}
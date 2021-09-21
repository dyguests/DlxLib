using System.Linq;

namespace SudokuConverterTest.Utils
{
    public static class SudokuUtil
    {
        public static string NumbersToString(this int[] numbers)
        {
            return string.Join(
                "\n",
                numbers
                    .Select((value, index) => new {value, index})
                    .GroupBy(x => x.index / 9)
                    .Select(x =>
                        string.Join("",
                            x.Select(y => y.value)
                                .Select(number => number > 0 ? "" + number : ".")
                        )
                    )
            );
        }
    }
}
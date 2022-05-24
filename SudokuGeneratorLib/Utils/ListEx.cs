using System.Collections.Generic;
using System.Linq;

namespace SudokuGeneratorLib.Utils
{
    public static class ListEx
    {
        public static List<T> ListOf<T>(params T[] item)
        {
            return item.ToList();
        }
    }
}
using System;

namespace SudokuLib
{
    internal static class Util
    {
        public static R Let<T, R>(this T self, Func<T, R> block)
        {
            return block(self);
        }
    }
}
using System;

namespace SudokuLib
{
    public static class Util
    {
        public static R Let<T, R>(this T self, Func<T, R> block)
        {
            return block(self);
        }   
    }
}
using System;
using SudokuDlxLib;
using SudokuDlxLib.Utils;

namespace SudokuTest.Utils
{
    internal static class MatrixUtil
    {
        public static void PrintMatrix(Matrix matrix)
        {
            Console.WriteLine("matrix:\n" + matrix.matrix.MatrixToString());
            Console.WriteLine("primaryColumns:\n" + matrix.primaryColumns?.ArrayToString());
            Console.WriteLine("secondaryColumns:\n" + matrix.secondaryColumns?.ArrayToString());
        }
    }
}
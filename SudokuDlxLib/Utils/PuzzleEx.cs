using System;
using SudokuLib;

namespace SudokuDlxLib.Utils
{
    public static class PuzzleEx
    {
        public static int GeneratePossibleDigits(this IPuzzle puzzle)
        {
            var size = puzzle.Size;
            var width = size[0];
            if (width != size[1])
            {
                throw new Exception("暂时仅支持正方形");
            }

            // var digits = 0b111_111_111;
            var digits = 0b0;
            for (var i = 0; i < width; i++)
            {
                digits |= (1 << i);
            }

            return digits;
        }

        public static int GetCandidateCount(this IPuzzle puzzle)
        {
            var size = puzzle.Size;
            var width = size[0];
            if (width != size[1])
            {
                throw new Exception("暂时仅支持正方形");
            }

            return width;
        }
    }
}
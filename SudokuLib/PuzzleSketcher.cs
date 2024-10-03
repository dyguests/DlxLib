﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuLib
{
    public static class PuzzleSketcher
    {
        private const char CharDigit1 = '1';
        private const char CharDigit9 = '9';
        private const char CharMask1 = 'a';

        public static IPuzzle FromSketch(string sketch)
        {
            var lines = sketch.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).Let(strings => new Queue<string>(strings));

            var digits = lines.Let(queue =>
            {
                // 不断从队列中 Dequeue，直到找到非空字符串为止
                while (queue.Count > 0)
                {
                    var dequeue = queue.Dequeue();
                    if (string.IsNullOrEmpty(dequeue)) continue;
                    return dequeue; // 找到非空字符串时返回
                }

                // 如果队列中全是空，返回 null
                return null;
            })?.Select(c => c is >= CharDigit1 and <= CharDigit9 ? c + 1 - CharDigit1 : 0).ToArray() ?? throw new Exception("sketch is empty");

            return new Puzzle(digits);
        }


        public static string ToSketch(IPuzzle puzzle, bool showSolution = true)
        {
            var enumerable = Enumerable.Range(0, puzzle.Digits.Length).Select(i =>
            {
                if (puzzle.Digits[i] != 0) return (char)(CharDigit1 + (puzzle.Digits[i] - 1));
                if (showSolution && puzzle.Solution[i] != 0) return (char)(CharMask1 + (puzzle.Solution[i] - 1));
                return '.';
            });
            var digitsSketch = string.Join("", enumerable);
            return digitsSketch;
        }
    }
}
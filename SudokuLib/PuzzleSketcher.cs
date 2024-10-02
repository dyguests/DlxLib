using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuLib
{
    public static class PuzzleSketcher
    {
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
            })?.Select(c => c is >= '0' and <= '9' ? c - '0' : 0).ToArray() ?? throw new Exception("sketch is empty");

            return new Puzzle(digits);
        }
    }
}
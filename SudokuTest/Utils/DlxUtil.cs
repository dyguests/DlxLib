using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using SudokuTest.Base;

namespace SudokuTest.Utils
{
    public static class DlxUtil
    {
        private const int tileCount = 9 * 9;
        private const int rowCount = 9 * 9;
        private const int colCount = 9 * 9;
        private const int boxCount = 9 * 9;

        public static int[,] ToMatrix(Puzzle puzzle)
        {
            var numbers = puzzle.numbers.Select(number => number.x).ToArray();

            var baseRows = GenerateBaseRows(puzzle);

            var rowArray = baseRows.ToArray();
            if (rowArray.Length == 0)
            {
                return new int[0, 0];
            }

            var matrix = new int[rowArray.Length, rowArray[0].Length];
            for (var i = 0; i < rowArray.Length; i++)
            {
                var length = rowArray[i].Length;
                for (var j = 0; j < length; j++)
                {
                    matrix[i, j] = rowArray[i][j];
                }
            }

            return matrix;
        }

        private static IEnumerable<int[]> GenerateBaseRows(Puzzle puzzle)
        {
            for (var i = 0; i < puzzle.numbers.Length; i++)
            {
                var possibleNumbers = FindPossibleNumbersAtIndex(puzzle, i);
                foreach (var possibleNumber in possibleNumbers)
                {
                    var row = new int[tileCount + rowCount + colCount + boxCount];
                    row[i] = 1;
                    row[tileCount + i / 9 * 9 + possibleNumber - 1] = 1;
                    row[tileCount + rowCount + i % 9 * 9 + possibleNumber - 1] = 1;
                    row[tileCount + rowCount + colCount + (i % 9 / 3 + i / 9 / 3 * 3) * 9 + possibleNumber - 1] = 1;
                    yield return row;
                }
            }
        }


        private static int[] FindPossibleNumbersAtIndex(Puzzle puzzle, int index)
        {
            var number = puzzle.numbers[index].x;
            if (number > 0)
            {
                return new[] {number};
            }

            var possibleNumbers = Enumerable.Range(1, 9)
                .Except(
                    puzzle.numbers.Select(_number => _number.x)
                        .Where((_number, _index) =>
                            _index % 9 == index % 9
                            || _index / 9 == index / 9
                            || (_index % 9 / 3 == index % 9 / 3 && _index / 9 / 3 == index / 9 / 3)
                        )
                );

            if (puzzle.rules != null)
            {
                // todo 
            }

            return possibleNumbers.ToArray();
        }

        public static int[] ToNumbers(int[,] matrix, int[] solution)
        {
            return solution.Select(index =>
                    matrix.GetRow(index).ParseToNumberAndIndex()
                )
                .OrderBy(numberAndIndex => numberAndIndex.y)
                .Select(numberAndIndex => numberAndIndex.x)
                .ToArray();
        }

        public static Vector2Int ParseToNumberAndIndex(this int[] row)
        {
            var index = Array.IndexOf(row, 1, 0, tileCount);
            int number = Array.IndexOf(row, 1, tileCount, rowCount) % 9 + 1;
            return new Vector2Int
            {
                x = number,
                y = index,
            };
        }

        public static string PuzzleToString(this Puzzle puzzle)
        {
            return string.Join(
                "\n",
                puzzle.numbers
                    .Select(number => number.x)
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
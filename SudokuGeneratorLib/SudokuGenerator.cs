using System;
using System.Linq;
using DlxLib;
using SudokuDlxLib;
using SudokuGeneratorLib.Utils;
using SudokuLib;

namespace SudokuGeneratorLib
{
    public static class SudokuGenerator
    {
        private static readonly Random Random = new Random();

        public static Sudoku GenerateNormalSudoku(int holeCount)
        {
            var solutionNumbers = GenerateSolution();
            var initNumbers = (int[]) solutionNumbers.Clone();
            HollowMatchNormalSudoku(initNumbers, holeCount);
            return new Sudoku
            {
                initNumbers = initNumbers,
                solutionNumbers = solutionNumbers,
                rules = new Rule[]
                {
                    new NormalRule(),
                },
            };
        }

        /// <summary>
        /// 新建一个numbers迷底
        /// </summary>
        /// <returns></returns>
        public static int[] GenerateSolution()
        {
            var board = new int[9, 9];
            for (var i = 0; i < board.GetLength(0); i++)
            {
                for (var j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = (i * 3 + i / 3 + j) % 9 + 1;
                }
            }

            Shuffle(board);

            return board.Flatten();
        }

        private static void Shuffle(int[,] board, int times = 15)
        {
            for (int i = 0; i < times; i++)
            {
                ShuffleOnce(board);
            }
        }

        private static void ShuffleOnce(int[,] board)
        {
            // 任选两个不同的数字，交换两种数字的位置
            var chooseNumber = -1;
            var replacingNumber = -1;
            while (replacingNumber == chooseNumber)
            {
                chooseNumber = Random.Next(1, 9 + 1);
                replacingNumber = Random.Next(1, 9 + 1);
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == chooseNumber)
                    {
                        board[i, j] = replacingNumber;
                    }
                    else if (board[i, j] == replacingNumber)
                    {
                        board[i, j] = chooseNumber;
                    }
                }
            }

            // 交换任意两行（同宫内）
            // 即1~3(4~6,7~9)行内部交换，但是不会出现第1行与第4行交换
            var sizeOfInnerMatrix = 3;
            var chooseRowIndex = -1;
            var replacingRowIndex = -1;
            while (chooseRowIndex == replacingRowIndex)
            {
                chooseRowIndex = Random.Next(0, sizeOfInnerMatrix);
                replacingRowIndex = Random.Next(0, sizeOfInnerMatrix);
            }

            var multiplier = Random.Next(0, sizeOfInnerMatrix);
            chooseRowIndex += (multiplier * sizeOfInnerMatrix);
            replacingRowIndex += (multiplier * sizeOfInnerMatrix);

            for (var i = 0; i < board.GetLength(1); i++)
            {
                var tmp = board[chooseRowIndex, i];
                board[chooseRowIndex, i] = board[replacingRowIndex, i];
                board[replacingRowIndex, i] = tmp;
            }

            //矩阵转置
            board.Transpose();
        }

        /// <summary>
        /// 挖洞
        ///
        /// 要保证挖完洞后，基于普通数独规则有唯一解
        /// </summary>
        /// <param name="initNumbers"></param>
        /// <param name="holeCount"></param>
        private static void HollowMatchNormalSudoku(int[] initNumbers, int holeCount)
        {
            var hollowedCount = HollowMatchNormalSudokuWithSimpleCheck(initNumbers, holeCount);
            HollowMatchNormalSudokuWithDlxCheck(initNumbers, holeCount - hollowedCount);
        }

        /// <summary>
        /// 挖洞
        ///
        /// 要保证挖完洞后，基于普通数独规则有唯一解
        /// 检查规则是，挖的洞基于行、列、宫是否唯一值
        /// </summary>
        /// <param name="sudokuNumbers"></param>
        /// <param name="holeCount"></param>
        /// <returns>挖了的洞的数量</returns>
        private static int HollowMatchNormalSudokuWithSimpleCheck(int[] sudokuNumbers, int holeCount)
        {
            return HollowMatchNormalSudokuWithCheck(sudokuNumbers, holeCount, (numbers, index) =>
            {
                return numbers.Where(i => i % 9 == index % 9
                                          || i / 9 == index / 9
                                          || (i % 9 / 3 == index % 9 / 3 && i / 9 / 3 == index / 9 / 3))
                           .Distinct()
                           .ToArray().Length == 8;
            });
        }

        private static int HollowMatchNormalSudokuWithDlxCheck(int[] sudokuNumbers, int holeCount)
        {
            return HollowMatchNormalSudokuWithCheck(sudokuNumbers, holeCount, (numbers, index) =>
            {
                var sudoku = new Sudoku
                {
                    initNumbers = sudokuNumbers,
                    rules = new Rule[]
                    {
                        new NormalRule(),
                    }
                };

                var matrix = SudokuDlxUtil.SudokuToMatrix(sudoku);
                var solutions = Dlx.Solve(matrix.matrix, matrix.primaryColumns, matrix.secondaryColumns).ToArray();
                return solutions.Length == 1;
            });
        }

        private static int HollowMatchNormalSudokuWithCheck(int[] initNumbers, int holeCount, Func<int[], int, bool> validator)
        {
            var removedItems = 0;
            foreach (var index in Enumerable.Range(0, 9 * 9)
                .OrderBy(i => Random.Next()))
            {
                if (removedItems >= holeCount)
                {
                    break;
                }

                var temp = initNumbers[index];
                if (temp == 0)
                {
                    continue;
                }

                initNumbers[index] = 0;
                if (validator(initNumbers, index))
                {
                    removedItems++;
                }
                else
                {
                    initNumbers[index] = temp;
                }
            }

            return removedItems;
        }
    }
}
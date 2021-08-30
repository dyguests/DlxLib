using System;
using SudokuGeneratorLib.Utils;

namespace SudokuGeneratorLib
{
    public class SudokuGenerator
    {
        private static readonly Random Random = new Random();

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
    }
}
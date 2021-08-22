using SudokuTest.Base;
using SudokuTest.Entities;

namespace SudokuTest.Datas
{
    public static class BaseLevelDatas
    {
        public static Puzzle[] simpleDatas =
        {
            CreatePuzzle("BCHFjEdGifJeDiGBHcdiGBCHFEjIGBCHfJDeJeDIGBCfHchFjEDIbgHFJedigcBEDiGBCHjFGbcHFjeID"),
        };

        private static Puzzle CreatePuzzle(string numberStr)
        {
            return new Puzzle
            {
                numbers = LevelNumbers2PuzzleNumbers(numberStr),
            };
        }

        private const byte s0 = (byte) 'A';
        private const byte h0 = (byte) 'a';

        private static Vector3Int[] LevelNumbers2PuzzleNumbers(string levelNumbers)
        {
            var numbers = new Vector3Int[9 * 9];
            for (var index = 0; index < levelNumbers.Length; index++)
            {
                var number = new Vector3Int();
                char c = levelNumbers[index];
                if (c < h0)
                {
                    byte v = (byte) (c - s0);
                    number.x = v;
                    number.y = v;
                    number.z = v;
                }
                else
                {
                    byte v = (byte) (c - h0);
                    number.z = v;
                }

                numbers[index] = number;
            }

            return numbers;
        }
    }
}
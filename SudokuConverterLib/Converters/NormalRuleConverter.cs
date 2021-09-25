using System.Text;
using SudokuLib;

namespace SudokuConverterLib.Converters
{
    public class NormalRuleConverter : IRuleConverter
    {
        private const byte S0 = (byte) 'A';
        private const byte H0 = (byte) 'a';

        private static NormalRuleConverter sInstance;

        private NormalRuleConverter()
        {
        }

        public static NormalRuleConverter GetInstance()
        {
            return sInstance ?? (sInstance = new NormalRuleConverter());
        }

        public string ToDataString(Sudoku sudoku, Rule rule)
        {
            var sb = new StringBuilder();
            for (var index = 0; index < sudoku.initNumbers.Length; index++)
            {
                sb.Append((char) ((sudoku.initNumbers[index] > 0 ? S0 : H0) + sudoku.solutionNumbers[index]));
            }

            return sb.ToString();
        }

        public Rule ToRule(string content) => null;

        public (int[] initNumbers, int[] solutionNumbers) ToSudokuNumbers(string levelNumbers)
        {
            int[] initNumbers = new int[9 * 9];
            int[] solutionNumbers = new int[9 * 9];

            for (var index = 0; index < levelNumbers.Length; index++)
            {
                char c = levelNumbers[index];
                byte value;
                if (c < H0)
                {
                    value = (byte) (c - S0);
                    initNumbers[index] = value;
                }
                else
                {
                    value = (byte) (c - H0);
                }

                solutionNumbers[index] = value;
            }

            return (initNumbers, solutionNumbers);
        }
    }
}
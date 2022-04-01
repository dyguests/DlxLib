using System.Linq;
using SudokuConverterLib.Converters;
using SudokuLib;

namespace SudokuConverterLib
{
    public static class SudokuConverter
    {
        private const byte S0 = (byte) 'A';
        private const byte H0 = (byte) 'a';

        /// <summary>
        /// 生成 str data,用于insert到表格中
        /// </summary>
        /// <param name="sudoku"></param>
        /// <returns></returns>
        public static string ToDataString(this Sudoku sudoku)
        {
            return string.Join(",", sudoku.rules.Select(rule => ConverterRouter.GetConverter(rule).ToDataString(sudoku, rule)).Select(Quote));

            string Quote(string content) => "\"" + content + "\"";
        }
    }
}
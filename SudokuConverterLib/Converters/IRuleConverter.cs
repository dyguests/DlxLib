using SudokuLib;

namespace SudokuConverterLib.Converters
{
    public interface IRuleConverter
    {
        string ToDataString(Sudoku sudoku, Rule rule);
        Rule ToRule(string content);
    }
}
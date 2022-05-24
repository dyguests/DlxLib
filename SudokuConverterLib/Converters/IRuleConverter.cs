using PuzzleLib;
using PuzzleLib.Rules;

namespace SudokuConverterLib.Converters
{
    public interface IRuleConverter
    {
        string ToDataString(Sudoku sudoku, Rule rule);
        Rule ToRule(string content);
    }
}
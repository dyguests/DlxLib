using SudokuLib;

namespace Databases.Converters
{
    public interface IRuleConverter
    {
        string ToDataString(Sudoku sudoku, Rule rule);
        Rule ToRule(string content);
    }
}
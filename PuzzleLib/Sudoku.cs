using System.Linq;
using PuzzleLib.Entities;

namespace PuzzleLib
{
    public class Sudoku : Puzzle
    {
        public override bool IsCompleted(Note note) => !solutionNumbers.Where((value, index) => value != initNumbers[index] && value != note.Numbers[index]).Any();
    }
}
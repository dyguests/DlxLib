using System.Linq;
using PuzzleLib.Entities;

namespace PuzzleLib
{
    public class Nurikabe : Puzzle
    {
        public override bool IsCompleted(Note note)
        {
            return !solutionNumbers.Where((value, index) =>
            {
                switch (value)
                {
                    case 1 when note.Numbers[index] == 1:
                    case 0 when note.Numbers[index] != 1:
                        return false;
                    default:
                        return true;
                }
            }).Any();
        }
    }
}
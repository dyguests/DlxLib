using SudokuLib;

namespace SudokuGeneratorLib;

public static class SudokuGenerator
{
    public static IPuzzle GenerateRandom()
    {
        return new Puzzle(Enumerable.Range(0, 81).ToArray());
    }
}
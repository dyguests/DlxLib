using NUnit.Framework;
using SudokuGeneratorLib;

namespace SudokuGeneratorTest
{
    public class SudokuGeneratorTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void GenerateRandom()
        {
            var puzzle = SudokuGenerator.GenerateRandom();
        }
    }
}
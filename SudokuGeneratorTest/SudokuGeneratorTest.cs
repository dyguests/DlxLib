using NUnit.Framework;
using SudokuGeneratorLib;

namespace SudokuGeneratorTest
{
    public class SudokuGeneratorTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void GenerateRandomFill()
        {
            var puzzle = SudokuGenerator.GenerateRandomFill();
            Assert.Pass();
        }
    }
}
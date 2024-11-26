using System;
using NUnit.Framework;
using SudokuGeneratorLib;
using SudokuLibTest;

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
            Console.WriteLine($"puzzle:\n{puzzle?.ToDisplay()}");
            Assert.Pass();
        }
    }
}
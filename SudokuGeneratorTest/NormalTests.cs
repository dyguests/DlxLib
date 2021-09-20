using System;
using NUnit.Framework;
using SudokuGeneratorLib;

namespace SudokuGeneratorTest
{
    [TestFixture]
    public class NormalTests
    {
        [Test]
        public void Test1()
        {
            var solutionNumbers = SudokuGenerator.GenerateSolution();
            Console.WriteLine("Solution:" + String.Join("", solutionNumbers));
            Assert.True(true);
        }
    }
}
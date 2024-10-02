using System;
using System.Linq;
using NUnit.Framework;
using SudokuLib;

namespace SudokuLibTest
{
    public class PuzzleSketcherTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void Test1()
        {
            const string sketch = "...........91......1..5.36..9..82..7.4..35..2..27..63..5.....4...1...9.....421...";
            var puzzle = PuzzleSketcher.FromSketch(sketch);
            Console.WriteLine($"sketch:{sketch}");
            var formattedDigits = puzzle.Digits
                .Select((digit, index) => new { digit, index })
                .GroupBy(x => x.index / 9, x => x.digit)
                .Select(group => string.Join("", group))
                .Aggregate((current, next) => current + Environment.NewLine + next);
            Console.WriteLine($"puzzle.Digits:{formattedDigits}");
            Assert.Pass();
        }
    }
}
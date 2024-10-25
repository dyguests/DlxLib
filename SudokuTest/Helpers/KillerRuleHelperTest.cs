using System;
using System.Linq;
using NUnit.Framework;
using SudokuLib.Helpers;
using SudokuLib.Rules;

namespace SudokuLibTest.Helpers
{
    public class KillerRuleHelperTest
    {
        [Test]
        public void Test_GetPossibleCombinations()
        {
            var cage = new KillerRule.Cage(10, new[] { 1, 2, });
            ValidateCage(cage, 4);
        }

        [Test]
        public void Test_GetPossibleCombinations2()
        {
            var cage = new KillerRule.Cage(45, new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            ValidateCage(cage, 1);
        }

        [Test]
        public void Test_GetPossibleCombinations3()
        {
            var cage = new KillerRule.Cage(45, new[] { 1, 2, 3, });
            ValidateCage(cage, 0);
        }

        [Test]
        public void Test_GetPossibleCombinations4()
        {
            var cage = new KillerRule.Cage(4, new[] { 1, 2, 3, });
            ValidateCage(cage, 0);
        }

        [Test]
        public void Test_GetPossibleCombinations5()
        {
            var cage = new KillerRule.Cage(15, new[] { 1, 2, 3, 4 });
            ValidateCage(cage, Enumerable.Range(1, 9).ToArray(), 6);
        }

        [Test]
        public void Test_GetPossibleCombinations20()
        {
            var cage = new KillerRule.Cage(10, new[] { 1, 2, });
            ValidateCage(cage, new[] { 1, 2, 8, 9 }, 2);
        }

        [Test]
        public void Test_GetPossibleCombinations21()
        {
            var cage = new KillerRule.Cage(10, new[] { 1, 2, });
            ValidateCage(cage, new[] { 1, 2, 6, 7 }, 0);
        }

        [Test]
        public void Test_GetPossibleNumbers()
        {
            var cage = new KillerRule.Cage(10, new[] { 1, 2, });
            var possibleDigits = KillerRuleHelper.GetPossibleDigits(cage);

            var expectedDigits = new[] { 1, 2, 3, 4, 6, 7, 8, 9 };
            Assert.That(possibleDigits, Is.EqualTo(expectedDigits));
        }

        [Test]
        public void Test_GetPossibleNumbers2()
        {
            var cage = new KillerRule.Cage(10, new[] { 1, 2, 3, 4, 5 });
            var possibleDigits = KillerRuleHelper.GetPossibleDigits(cage);

            var expectedDigits = Array.Empty<int>();
            Assert.That(possibleDigits, Is.EqualTo(expectedDigits));
        }

        private static void ValidateCage(KillerRule.Cage cage, int expected)
        {
            Console.WriteLine($"cage:{cage}");

            var combinations = KillerRuleHelper.GetPossibleCombinations(cage).ToArray();
            foreach (var combination in combinations)
            {
                Console.WriteLine($"combination:{string.Join(",", combination)}");
            }

            Assert.That(combinations.Length, Is.EqualTo(expected));
        }

        private static void ValidateCage(KillerRule.Cage cage, int[] possibleDigits, int expected)
        {
            Console.WriteLine($"cage:{cage}");

            var combinations = KillerRuleHelper.GetPossibleCombinations(cage, possibleDigits).ToArray();
            foreach (var combination in combinations)
            {
                Console.WriteLine($"combination:{string.Join(",", combination)}");
            }

            Assert.That(combinations.Length, Is.EqualTo(expected));
        }
    }
}
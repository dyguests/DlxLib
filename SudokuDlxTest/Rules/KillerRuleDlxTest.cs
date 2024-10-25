using System;
using System.Linq;
using NUnit.Framework;
using SudokuDlxLib.Rules;

namespace SudokuDlxLibTest.Rules
{
    public class KillerRuleDlxTest
    {
        [Test]
        public void Test_GetPossiblePermutations()
        {
            var permutations = KillerRuleDlx.TestGetPossiblePermutations(
                new[] { 1, 2, 3 },
                new[] { new[] { 1, 2, 3 }, new[] { 1, 2, 3 }, new[] { 1, 2, 3 } }
            ).ToArray();

            foreach (var permutation in permutations)
            {
                Console.WriteLine(string.Join(",", permutation));
            }

            Assert.That(permutations.Length, Is.EqualTo(6));
        }

        [Test]
        public void Test_GetPossiblePermutations2()
        {
            var permutations = KillerRuleDlx.TestGetPossiblePermutations(
                new[] { 1, 2, 3 },
                new[] { new[] { 2, 3 }, new[] { 1, 2, 3 }, new[] { 1, 2, 3 } }
            ).ToArray();

            foreach (var permutation in permutations)
            {
                Console.WriteLine(string.Join(",", permutation));
            }

            Assert.That(permutations.Length, Is.EqualTo(4));
        }

        [Test]
        public void Test_GetPossiblePermutations3()
        {
            var permutations = KillerRuleDlx.TestGetPossiblePermutations(
                new[] { 1, 2, 3 },
                new[] { new[] { 1, 2, }, new[] { 1, 2, 3 }, new[] { 1, 2, 3 } }
            ).ToArray();

            foreach (var permutation in permutations)
            {
                Console.WriteLine(string.Join(",", permutation));
            }

            Assert.That(permutations.Length, Is.EqualTo(4));
        }

        [Test]
        public void Test_GetPossiblePermutations4()
        {
            var permutations = KillerRuleDlx.TestGetPossiblePermutations(
                new[] { 1, 2 },
                new[] { new[] { 1, }, new[] { 1, 2 } }
            ).ToArray();

            foreach (var permutation in permutations)
            {
                Console.WriteLine(string.Join(",", permutation));
            }

            Assert.That(permutations.Length, Is.EqualTo(1));
        }
    }
}
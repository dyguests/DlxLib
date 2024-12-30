using System;
using System.Linq;
using NUnit.Framework;
using SudokuDlxLib.Rules;
using SudokuLib;
using SudokuLib.Rules;

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

        [Test]
        public void Test_ExpandRows1()
        {
            var cage = new KillerRule.Cage(20, new[] { 0, 1, 2 });
            var killerRule = new KillerRule(cage);
            var puzzle = new Puzzle(new int[81], killerRule);
            var rows = new[]
            {
                new[] { 1, 0, 0, 0, 0b111_111_111, },
                new[] { 0, 1, 0, 1, 0b111_111_111, },
                new[] { 0, 0, 1, 2, 0b111_111_111, },
            };
            var columnPredicate = new[] { 0, 0, 0, 81, 9, };
            var (newRows, newColumnPredicate) = new KillerRuleDlx().ExpandRows(puzzle, rows, columnPredicate);
            Console.WriteLine("newRow:");
            foreach (var newRow in newRows)
            {
                Console.WriteLine(string.Join(",", newRow));
            }

            Console.WriteLine("newColumnPredicate:");
            Console.WriteLine(string.Join(",", newColumnPredicate));
        }
    }
}
using System;
using System.Linq;
using NUnit.Framework;
using SudokuDlxLib;
using SudokuDlxLib.Rules;

namespace SudokuDlxLibTest
{
    [Obsolete]
    public class MathUtilTest
    {
        [Test]
        public void Test_GetPermutations()
        {
            var permutations = MathUtil.GetPermutations(new int[] { 1, 2, 3 }).ToArray();
            foreach (var permutation in permutations)
            {
                Console.WriteLine(string.Join(",", permutation));
            }

            Assert.That(permutations.Length, Is.EqualTo(6));
        }
    }
}
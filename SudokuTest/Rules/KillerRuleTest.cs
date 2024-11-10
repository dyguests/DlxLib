using NUnit.Framework;
using SudokuLib.Rules;

namespace SudokuLibTest.Rules
{
    public class KillerRuleTest
    {
        [Test]
        public void TestKillerRule()
        {
            var input = "Killer 3=12+13;0=1+2";
            ValidateKillerRuleSketch(input);
        }

        [Test]
        public void TestKillerRule2()
        {
            var input = "Killer ";
            ValidateKillerRuleSketch(input);
        }

        [Test]
        public void TestKillerRule3()
        {
            var input = "Killer 3=12+13";
            ValidateKillerRuleSketch(input);
        }

        [Test]
        public void TestKillerRule4()
        {
            var input = "Killer 3=12+13;3=12+13;3=12+13;3=12+13;3=12+13";
            ValidateKillerRuleSketch(input);
        }

        private static void ValidateKillerRuleSketch(string input)
        {
            var rule = (KillerRule)new KillerRule().FromSketch(input);
            var actual = rule.ToSketch();
            Assert.That(actual, Is.EqualTo(input));
        }

        [Test]
        public void TestCage()
        {
            var input = "3=12+13";
            ValidateCageSketch(input);
        }

        [Test]
        public void TestCage2()
        {
            var input = "3=12";
            ValidateCageSketch(input);
        }

        [Test]
        public void TestCage3()
        {
            var input = "3=12+23+34+56+78+1+2";
            ValidateCageSketch(input);
        }

        private static void ValidateCageSketch(string input)
        {
            var cage = KillerRule.Cage.Instance.FromSketch(input);
            var actual = cage?.ToSketch();
            Assert.That(actual, Is.EqualTo(input));
        }
    }
}
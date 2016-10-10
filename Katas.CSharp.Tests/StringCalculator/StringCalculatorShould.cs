using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Katas.CSharp.Tests.StringCalculator
{
    [TestFixture]
    public class StringCalculatorShould
    {
        private StringCalculator _sut;

        [SetUp]
        public void TestSetup()
        {
            _sut = new StringCalculator();
        }

        [TestCase("")]
        [TestCase("     ")]
        [TestCase(null)]
        public void Return0ForEmptyStrings(string problem)
        {
            var result = _sut.Add(problem);
            result.Should().Be(0);
        }

        [TestCase(1, 2, 3)]
        public void AddTwoNumbersDelimitedByAComma(int firstNumber, int secondNumber, int expectedAdditionResult)
        {
            string problem = $"{firstNumber},{secondNumber}";
            var result = _sut.Add(problem);
            result.Should().Be(expectedAdditionResult);
        }

        [TestCase("1,2,3,1,2,3")]
        [TestCase("1\n2\n3\n1\n2\n3")]
        [TestCase("1,2\n3,1\n2,3")]
        public void AddNumbersDelimetedByNewLineOrCommas(string problem)
        {
            var result = _sut.Add(problem);
            result.Should().Be(12);
        }
    }

    public class StringCalculator
    {
        public int Add(string problem)
        {
            if (string.IsNullOrWhiteSpace(problem))
            {
                return 0;
            }

            return problem.Split(',', '\n').Select(int.Parse).Sum();
        }
    }
}

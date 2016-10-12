using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
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

        [TestCase(';')]
        [TestCase('*')]
        public void SupportCustomDelimeters(char customDelimeter)
        {
            var addsTo12 = new[] { 1, 2, 3, 1, 2, 3 };
            var problem = "//" + customDelimeter + "\n" + string.Join(customDelimeter.ToString(), addsTo12);

            var result = _sut.Add(problem);

            result.Should().Be(12);
        }

        [TestCase("****")]
        public void SupportsVariantLengthCustomDelimeters(string variantLengthCustomDelimeter)
        {
            var addsTo12 = new[] { 1, 2, 3, 1, 2, 3 };
            var problem = $"//[" + variantLengthCustomDelimeter + "]\n" + string.Join(variantLengthCustomDelimeter, addsTo12);

            var result = _sut.Add(problem);

            result.Should().Be(12);
        }

        [TestCase("****, %, ||")]
        [TestCase("*, !")]
        public void SupportsMultipleVariantLengthCustomDelimeters(string possibleDelimetersSpec)
        {
            var customDelimeters = possibleDelimetersSpec.Split(',').Select(customDelimeter => customDelimeter.Trim()).ToArray();
            var addsTo12 = new[] { 1, 2, 3, 1, 2, 3 };

            var delimeterSpecification = "//" + string.Join(string.Empty, customDelimeters.Select(del => "[" + del + "]"));
            var problemSpecification = string.Join(customDelimeters.First(), customDelimeters.Select(delimeter => string.Join(delimeter, addsTo12)));
            var problem = delimeterSpecification + "\n" + problemSpecification;

            var result = _sut.Add(problem);

            result.Should().Be(12 * customDelimeters.Length);
        }

        [TestCase("1,-2,3,1,2,3", "-2")]
        [TestCase("1,-2,-3,1,2,-3", "-2, -3, -3")]
        public void NegativeNumbersThrowExceptions(string problem, string expectedErrorMessageSubstring)
        {
            var thrownException = Assert.Throws<Exception>(() => _sut.Add(problem));
            thrownException.Message.Should().Contain("negatives not allowed");
            thrownException.Message.Should().Contain(expectedErrorMessageSubstring);
        }

        [TestCase("2, 1002, 5", 7)]
        [TestCase("2, 1000, 5", 1007)]
        [TestCase("2, 999, 5", 1006)]
        public void NumbersGreatherThan1000ShouldBeIgnored(string problem, int expectedSum)
        {
            var result = _sut.Add(problem);
            result.Should().Be(expectedSum);
        }
    }
}

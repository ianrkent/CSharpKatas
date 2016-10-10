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
        public void SupportSupportCustomDelimeters(char customDelimeter)
        {
            var addsTo12 = new[] { 1, 2, 3, 1, 2, 3 };
            var problem = "//" + customDelimeter + "\n" + string.Join(customDelimeter.ToString(), addsTo12);

            var result = _sut.Add(problem);
            result.Should().Be(12);
        }

        [TestCase("1,-2,3,1,2,3", "-2")]
        public void NegativeNumbersThrowExceptions(string problem, string expectedErrorMessageSubstring)
        {
            var thrownException = Assert.Throws<Exception>(() => _sut.Add(problem));
            thrownException.Message.Should().Contain("negatives not allowed");
            thrownException.Message.Should().Contain(expectedErrorMessageSubstring);
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

            var delimeter = ",";

            if (UsesCustomDelimeter(problem))
            {
                problem = ExtractDelimeterFromProblem(problem, ref delimeter);
            }

            return problem
                    .Split(new[] { delimeter, "\n" }, StringSplitOptions.None)
                    .Select(int.Parse)
                    .Sum();
        }

        private static string ExtractDelimeterFromProblem(string problem, ref string delimeter)
        {
            var firstsNewLineIndex = problem.IndexOf('\n');
            delimeter = problem.Substring(2, firstsNewLineIndex - 2);
            problem = problem.Substring(firstsNewLineIndex + 1);
            return problem;
        }

        private static bool UsesCustomDelimeter(string problem)
        {
            return problem.Substring(0, 2) == "//";
        }
    }
}

using FluentAssertions;
using NUnit.Framework;

namespace Katas.CSharp.Tests.FizzBuzz
{
    [TestFixture]
    public class FizzBuzzShould
    {
        [TestCase(1, "1")]
        [TestCase(2, "2")]
        [TestCase(3, "Fizz")]
        [TestCase(6, "Fizz")]
        [TestCase(9, "Fizz")]
        [TestCase(5, "Buzz")]
        [TestCase(15, "FizzBuzz")]
        [TestCase(30, "FizzBuzz")]
        public void ReturnCorrectValue(int value, string expectedResult)
        {
            // arrange
            var fizzBuzzCalculator = new FizzBuzzCalculator();
            
            // act
            string result = fizzBuzzCalculator.FizzBuzzMe(value);

            // assert
            result.Should().Be(expectedResult);
        }
    }
}

using FluentAssertions;
using NUnit.Framework;

namespace Katas.CSharp.Tests.Bowling
{
    public class BowlingScoreCalcuatorShould
    {
        [TestCase("--|--|--|--|--|--|--|--|--|--||", 0)]
        [TestCase("--|--|--|--|--|1-|--|--|--|--||", 1)]
        [TestCase("1-|1-|1-|1-|1-|1-|1-|1-|1-|1-||", 10)]
        [TestCase("1-|1-|1-|1-|1-|18|1-|1-|1-|1-||", 18)]
        [TestCase("9-|9-|9-|9-|9-|9-|9-|9-|9-|9-||", 90)]
        [TestCase("1/|--|--|--|--|--|--|--|--|--||", 10)]
        [TestCase("6/|--|--|--|--|--|--|--|--|--||", 10)]
        [TestCase("6/|2-|--|--|--|--|--|--|--|--||", 14)]
        [TestCase("6/|1-|--|--|--|--|--|--|--|--||", 12)]
        [TestCase("X|--|--|--|--|--|--|--|--|--||", 10)]
        [TestCase("X|22|--|--|--|--|--|--|--|--||", 18)]
        [TestCase("X|X|X|--|--|--|--|--|--|--||", 60)]
        [TestCase("X|X|X|X|X|X|X|X|X|X||XX", 300)]
        [TestCase("5/|5/|5/|5/|5/|5/|5/|5/|5/|5/||5", 150)]
        [TestCase("X|X|X|X|X|X|X|X|X|2/||X", 272)]
        public void CalculateTheScoreIteratively(string scoreCard, int expectedScore)
        {
            var sut = new IterativeBowlingScoreCalcuator();

            var iterativelyCalculatedScore = sut.CalculateScoreIteratively(scoreCard);

            iterativelyCalculatedScore.Should().Be(expectedScore);
        }

        [TestCase("--|--|--|--|--|--|--|--|--|--||", 0)]
        [TestCase("--|--|--|--|--|1-|--|--|--|--||", 1)]
        [TestCase("1-|1-|1-|1-|1-|1-|1-|1-|1-|1-||", 10)]
        [TestCase("1-|1-|1-|1-|1-|18|1-|1-|1-|1-||", 18)]
        [TestCase("9-|9-|9-|9-|9-|9-|9-|9-|9-|9-||", 90)]
        [TestCase("1/|--|--|--|--|--|--|--|--|--||", 10)]
        [TestCase("6/|--|--|--|--|--|--|--|--|--||", 10)]
        [TestCase("6/|2-|--|--|--|--|--|--|--|--||", 14)]
        [TestCase("6/|1-|--|--|--|--|--|--|--|--||", 12)]
        [TestCase("X|--|--|--|--|--|--|--|--|--||", 10)]
        [TestCase("X|22|--|--|--|--|--|--|--|--||", 18)]
        [TestCase("X|X|X|--|--|--|--|--|--|--||", 60)]
        [TestCase("X|X|X|X|X|X|X|X|X|X||XX", 300)]
        [TestCase("5/|5/|5/|5/|5/|5/|5/|5/|5/|5/||5", 150)]
        [TestCase("X|X|X|X|X|X|X|X|X|2/||X", 272)]
        public void CalculateTheScoreRecursively(string scoreCard, int expectedScore)
        {
            var sut = new RecursiveBowlingScoreCalcuator();

            var recursivelyCalculatedScore = sut.CalculateScore(scoreCard);

            recursivelyCalculatedScore.Should().Be(expectedScore);
        }
    }
}

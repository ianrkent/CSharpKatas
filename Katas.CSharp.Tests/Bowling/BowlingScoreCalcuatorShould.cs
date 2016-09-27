using System.Linq;
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
        public void CalculateTheScore(string scoreCard, int expectedScore)
        {
            var sut = new BowlingScoreCalcuator();

            var iterativelyCalculatedScore = sut.CalculateScoreIteratively(scoreCard);

            iterativelyCalculatedScore.Should().Be(expectedScore);
        }
    }

    public class BowlingScoreCalcuator  
    {
        public int CalculateScoreIteratively(string scoreCard)
        {
            var turnScore = scoreCard.Replace("|", string.Empty).ToCharArray();
            var multiplyBy = Enumerable.Repeat(1, turnScore.Length).ToArray();

            for (var i = 0; i < turnScore.Length; i++)
            {
                if (turnScore[i] == 'X')
                {
                    multiplyBy[i + 1]++;
                    multiplyBy[i + 2]++;
                }

                if (turnScore[i] == '/')
                {
                    multiplyBy[i + 1]++;
                }
            }

            var pinsKnockedDown = turnScore
                .Select((rollResult, rollIndex) => PinScoreToValue(rollResult, rollIndex > 0 ? turnScore[rollIndex - 1] : '-'))
                .ToArray();

            return pinsKnockedDown
                .Select((pinCount, i) => pinCount * multiplyBy[i])
                .Sum();
        }

        private static int PinScoreToValue(char pinScore, char previousPinScore)
        {
            if (pinScore == '-') return 0;
            if (pinScore == 'X') return 10;
            if (pinScore == '/') return 10 - int.Parse(previousPinScore.ToString());

            return int.Parse(pinScore.ToString());
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace Katas.CSharp.Tests.Bowling
{
    public class RecursiveBowlingScoreCalcuator
    {
        public int CalculateScore(string scoreCard)
        {
            var balls = ScoreCardParser.ExtractBallInformation(scoreCard);

            var reverseBalls = balls.Reverse();

            return CalculateScoreHelper(reverseBalls, BallInformation.NullBall, BallInformation.NullBall);
        }

        private int CalculateScoreHelper(IEnumerable<BallInformation> remainingBallsInReverse, BallInformation lastBall, BallInformation secondToLastBall)
        {
            if (!remainingBallsInReverse.Any())
            {
                return 0;
            }

            var ball = remainingBallsInReverse.First();

            if (ball.IsExtraBall)
            {
                return CalculateScoreHelper(remainingBallsInReverse.Skip(1), ball, lastBall);
            }

            var ballScoreContribution = 
                ball.IsStrike
                    ? ball.PinsKnockedDown + lastBall.PinsKnockedDown + secondToLastBall.PinsKnockedDown
                    : ball.IsSpare
                        ? ball.PinsKnockedDown + lastBall.PinsKnockedDown
                        : ball.PinsKnockedDown;

            return ballScoreContribution + CalculateScoreHelper(remainingBallsInReverse.Skip(1), ball, lastBall);
        }
    }
}
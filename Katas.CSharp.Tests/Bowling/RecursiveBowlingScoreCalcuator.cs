using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace Katas.CSharp.Tests.Bowling
{
    public class RecursiveBowlingScoreCalcuator
    {
        public int CalculateScore(string scoreCard)
        {
            var theSplit = scoreCard.Split(new[] {"||"}, StringSplitOptions.None);

            var normalBalls = theSplit[0].Replace("|", string.Empty);

            var normalBallResults = normalBalls.Select(
                (ball, index) => new BallResult(ball, index == 0 ? '-' : normalBalls[index-1]));

            var extraBallResults = theSplit[1]
                .Select(ball => new BallResult(ball) { IsExtraBall = true });

            var ballsInReverse = normalBallResults.Union(extraBallResults).Reverse();

            return CalculateScoreHelper(ballsInReverse, BallResult.NullBall, BallResult.NullBall);
        }

        private int CalculateScoreHelper(IEnumerable<BallResult> remainingBallsInReverse, BallResult lastBall, BallResult secondToLastBall)
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
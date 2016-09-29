using System;
using System.Linq;

namespace Katas.CSharp.Tests.Bowling
{
    public class IterativeBowlingScoreCalcuator  
    {
        public int CalculateScoreIteratively(string scoreCard)
        {
            var balls = ScoreCardParser.ExtractBallInformation(scoreCard).ToArray();

            var multiplyBy = balls.Select(ball => ball.IsExtraBall ? 0 : 1).ToArray();

            for (var i = 0; i < balls.Count(); i++)
            {
                var ball = balls[i];

                if (ball.IsStrike && !ball.IsExtraBall)
                {
                    multiplyBy[i + 1]++;
                    multiplyBy[i + 2]++;
                }

                if (ball.IsSpare)
                {
                    multiplyBy[i + 1]++;
                }
            }

            return balls.Select((ball, i) => ball.PinsKnockedDown * multiplyBy[i]).Sum();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Katas.CSharp.Tests.Bowling
{
    public class ScoreCardParser
    {
        public static IEnumerable<BallInformation> ExtractBallInformation(string scoreCard)
        {
            var theSplit = scoreCard.Split(new[] {"||"}, StringSplitOptions.None);

            var normalBalls = theSplit[0].Replace("|", string.Empty);

            var normalBallResults = normalBalls.Select(
                (ball, index) => new BallInformation(ball, index == 0 ? '-' : normalBalls[index - 1]));

            var extraBallResults = theSplit[1]
                .Select(ball => new BallInformation(ball) { IsExtraBall = true });

            return normalBallResults.Union(extraBallResults);
        }
    }
}
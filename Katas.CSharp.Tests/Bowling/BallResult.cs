﻿namespace Katas.CSharp.Tests.Bowling
{
    public class BallResult
    {
        public static readonly BallResult NullBall = new BallResult();

        private BallResult()
        {
        }

        public BallResult(char ballScore, char previousBallResult = '-')
        {
            PinsKnockedDown = ballScore == '/'
                ? 10 - ConvertNonSpare(previousBallResult)
                : ConvertNonSpare(ballScore);

            IsStrike = ballScore == 'X';
            IsSpare = ballScore == '/';
        }

        public bool IsStrike  { get; }

        public bool IsSpare { get; set; }

        public bool IsExtraBall { get; set; }

        public int PinsKnockedDown { get; }

        private int ConvertNonSpare(char ballScore)
        {
            if (ballScore == 'X')
            {
                return 10;
            }

            if (ballScore == '-')
            {
                return 0;
            }

            return int.Parse(ballScore.ToString());
        }
    }
}
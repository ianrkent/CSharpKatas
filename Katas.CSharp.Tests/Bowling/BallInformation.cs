namespace Katas.CSharp.Tests.Bowling
{
    public class BallInformation
    {
        public static readonly BallInformation NullBall = new BallInformation();

        private BallInformation()
        {
        }

        public BallInformation(char ballScore, char previousBallResult = '-')
        {
            PinsKnockedDown = 
                ballScore == '/'
                    ? 10 - ConvertNonSpare(previousBallResult)
                    : ConvertNonSpare(ballScore);

            IsStrike = ballScore == 'X';
            IsSpare = ballScore == '/';
        }

        public bool IsStrike  { get; }

        public bool IsSpare { get; set; }

        public bool IsExtraBall { get; set; }

        public int PinsKnockedDown { get; }

        public override string ToString()
        {
            return PinsKnockedDown + (IsStrike ? "X" : string.Empty) + (IsSpare ? "/" : string.Empty) +
                   (IsExtraBall ? "*" : string.Empty);
        }

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
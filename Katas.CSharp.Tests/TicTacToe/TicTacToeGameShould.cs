using NUnit.Framework;

namespace Katas.CSharp.Tests.TicTacToe
{
    [TestFixture]
    public class TicTakToeGameShould
    {
        [Test]
        public void EnforceXToBePlayedFirst()
        {
            var moveResult = new TicTakToeGame().MakeAPlay(Token.X, BoardPosition.BottomCentre);

            Assert.AreEqual(MoveResult.InProgress, moveResult);
        }

        [Test]
        public void EnforceOToBePlayedSecond()
        {
            var board = new TicTakToeGame();
            board.MakeAPlay(Token.X, BoardPosition.TopRight);

            var moveResult = board.MakeAPlay(Token.O, BoardPosition.TopCentre);

            Assert.AreEqual(MoveResult.InProgress, moveResult);
        }

        [Test]
        public void EnforceXToBePlayedThird()
        {
            var board = new TicTakToeGame();
            board.MakeAPlay(Token.X, BoardPosition.BottomRight);
            board.MakeAPlay(Token.O, BoardPosition.MiddleLeft);
            var moveResult = board.MakeAPlay(Token.O, BoardPosition.TopCentre);

            Assert.AreEqual(MoveResult.InvalidMove, moveResult);
        }

        [Test]
        public void EnforceOToBePlayedForth()
        {
            var board = new TicTakToeGame();
            board.MakeAPlay(Token.X, BoardPosition.BottomRight);
            board.MakeAPlay(Token.O, BoardPosition.MiddleLeft);
            board.MakeAPlay(Token.X, BoardPosition.TopLeft);
            var moveResult = board.MakeAPlay(Token.X, BoardPosition.TopCentre);

            Assert.AreEqual(MoveResult.InvalidMove, moveResult);
        }

        [Test]
        public void NotAllowOToBePlayedFirst()
        {
            var moveResult = new TicTakToeGame().MakeAPlay(Token.O, BoardPosition.BottomCentre);
            Assert.AreEqual(MoveResult.InvalidMove, moveResult);
        }

        [Test]
        public void NotAllowPlaysOnTheSamePositionPlayedLast()
        {
            var board = new TicTakToeGame();

            board.MakeAPlay(Token.X, BoardPosition.BottomCentre);

            var moveResult = board.MakeAPlay(Token.O, BoardPosition.BottomCentre);

            Assert.AreEqual(MoveResult.InvalidMove, moveResult);
        }

        [Test]
        public void NotAllowPlaysOnAPositionPlayed2StepsAgo()
        {
            var board = new TicTakToeGame();
            board.MakeAPlay(Token.X, BoardPosition.TopRight);
            board.MakeAPlay(Token.O, BoardPosition.TopCentre);

            var moveResult = board.MakeAPlay(Token.X, BoardPosition.TopRight);

            Assert.AreEqual(MoveResult.InvalidMove, moveResult);
        }

        [Test]
        public void ReportAWinningMoveWhen3XInTopRow()
        {
            var board = new TicTakToeGame();
            board.MakeAPlay(Token.X, BoardPosition.TopLeft);
            board.MakeAPlay(Token.O, BoardPosition.BottomLeft);
            board.MakeAPlay(Token.X, BoardPosition.TopCentre);
            board.MakeAPlay(Token.O, BoardPosition.BottomCentre);
            var result = board.MakeAPlay(Token.X, BoardPosition.TopRight);

            Assert.AreEqual(MoveResult.YouAreAWinner, result);
        }

        [Test]
        public void ReportAWinningMoveWhen3OInBottomRow()
        {
            var board = new TicTakToeGame();
            board.MakeAPlay(Token.X, BoardPosition.TopLeft);
            board.MakeAPlay(Token.O, BoardPosition.BottomLeft);
            board.MakeAPlay(Token.X, BoardPosition.TopCentre);
            board.MakeAPlay(Token.O, BoardPosition.BottomCentre);
            board.MakeAPlay(Token.X, BoardPosition.MiddleCentre);
            var result = board.MakeAPlay(Token.O, BoardPosition.BottomRight);

            Assert.AreEqual(MoveResult.YouAreAWinner, result);
        }

        [Test]
        public void ReportAWinningMoveWhen3OInMiddleRow()
        {
            var board = new TicTakToeGame();
            board.MakeAPlay(Token.X, BoardPosition.TopLeft);
            board.MakeAPlay(Token.O, BoardPosition.MiddleLeft);
            board.MakeAPlay(Token.X, BoardPosition.TopCentre);
            board.MakeAPlay(Token.O, BoardPosition.MiddleCentre);
            board.MakeAPlay(Token.X, BoardPosition.BottomLeft);
            var result = board.MakeAPlay(Token.O, BoardPosition.MiddleRight);

            Assert.AreEqual(MoveResult.YouAreAWinner, result);
        }

        [Test]
        public void ReportAWinningMoveWhen3XInBackslashDiagonal()
        {
            var board = new TicTakToeGame();
            board.MakeAPlay(Token.X, BoardPosition.TopLeft);
            board.MakeAPlay(Token.O, BoardPosition.MiddleLeft);
            board.MakeAPlay(Token.X, BoardPosition.MiddleCentre);
            board.MakeAPlay(Token.O, BoardPosition.TopCentre);
            var result = board.MakeAPlay(Token.X, BoardPosition.BottomRight);

            Assert.AreEqual(MoveResult.YouAreAWinner, result);
        }

        [Test]
        public void ReportAWinningMoveWhen3XInForwardslashDiagonal()
        {
            var board = new TicTakToeGame();
            board.MakeAPlay(Token.X, BoardPosition.BottomLeft);
            board.MakeAPlay(Token.O, BoardPosition.MiddleLeft);
            board.MakeAPlay(Token.X, BoardPosition.MiddleCentre);
            board.MakeAPlay(Token.O, BoardPosition.TopCentre);
            var result = board.MakeAPlay(Token.X, BoardPosition.TopRight);

            Assert.AreEqual(MoveResult.YouAreAWinner, result);
        }

        [Test]
        public void ReportAWinningMoveWhen3XInLeftColumn()
        {
            var board = new TicTakToeGame();
            board.MakeAPlay(Token.X, BoardPosition.BottomLeft);
            board.MakeAPlay(Token.O, BoardPosition.MiddleCentre);
            board.MakeAPlay(Token.X, BoardPosition.MiddleLeft);
            board.MakeAPlay(Token.O, BoardPosition.TopCentre);
            var result = board.MakeAPlay(Token.X, BoardPosition.TopLeft);

            Assert.AreEqual(MoveResult.YouAreAWinner, result);
        }

        [Test]
        public void ReportAWinningMoveWhen3XInMiddleColumn()
        {
            var board = new TicTakToeGame();
            board.MakeAPlay(Token.X, BoardPosition.BottomCentre);
            board.MakeAPlay(Token.O, BoardPosition.TopRight);
            board.MakeAPlay(Token.X, BoardPosition.TopCentre);
            board.MakeAPlay(Token.O, BoardPosition.BottomLeft);
            var result = board.MakeAPlay(Token.X, BoardPosition.MiddleCentre);

            Assert.AreEqual(MoveResult.YouAreAWinner, result);
        }

        [Test]
        public void ReportAWinningMoveWhen3XInRightColumn()
        {
            var board = new TicTakToeGame();
            board.MakeAPlay(Token.X, BoardPosition.TopRight);
            board.MakeAPlay(Token.O, BoardPosition.MiddleCentre);
            board.MakeAPlay(Token.X, BoardPosition.BottomRight);
            board.MakeAPlay(Token.O, BoardPosition.TopCentre);
            var result = board.MakeAPlay(Token.X, BoardPosition.MiddleRight);

            Assert.AreEqual(MoveResult.YouAreAWinner, result);
        }

        [Test]
        public void ReportADrawWhenYouHavePlayedTheLastPositionAndNotWon()
        {
            var board = new TicTakToeGame();
            board.MakeAPlay(Token.X, BoardPosition.BottomLeft);
            board.MakeAPlay(Token.O, BoardPosition.MiddleLeft);
            board.MakeAPlay(Token.X, BoardPosition.TopLeft);
            board.MakeAPlay(Token.O, BoardPosition.TopCentre);
            board.MakeAPlay(Token.X, BoardPosition.MiddleCentre);
            board.MakeAPlay(Token.O, BoardPosition.BottomRight); 
            board.MakeAPlay(Token.X, BoardPosition.MiddleRight);
            board.MakeAPlay(Token.O, BoardPosition.TopRight);
            var result = board.MakeAPlay(Token.X, BoardPosition.BottomCentre);

            Assert.AreEqual(MoveResult.NoWinnerYouDrawers, result);
        }
    }
}

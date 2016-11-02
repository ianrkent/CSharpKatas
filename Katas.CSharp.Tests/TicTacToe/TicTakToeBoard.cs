using System.Linq;

namespace Katas.CSharp.Tests.TicTacToe
{
    public class TicTakToeBoard
    {
        private readonly Token[] _tokenPositions = new Token[9];

        private static readonly BoardPosition[][] WinningStripes = {
                new[] { BoardPosition.TopLeft, BoardPosition.TopCentre, BoardPosition.TopRight },
                new[] { BoardPosition.MiddleLeft, BoardPosition.MiddleCentre, BoardPosition.MiddleRight },
                new[] { BoardPosition.BottomLeft, BoardPosition.BottomCentre, BoardPosition.BottomRight },
                new[] { BoardPosition.TopLeft, BoardPosition.MiddleCentre, BoardPosition.BottomRight },
                new[] { BoardPosition.BottomLeft, BoardPosition.MiddleCentre, BoardPosition.TopRight },
                new[] { BoardPosition.BottomLeft, BoardPosition.MiddleLeft, BoardPosition.TopLeft },
                new[] { BoardPosition.BottomCentre, BoardPosition.MiddleCentre, BoardPosition.TopCentre },
                new[] { BoardPosition.BottomRight, BoardPosition.MiddleRight, BoardPosition.TopRight }
            };

        public bool TryPlaceToken(Token token, BoardPosition boardPosition)
        {
            if (!CanPlaceToken(token, boardPosition))
                return false;

            _tokenPositions[(int)boardPosition] = token;

            return true;
        }

        public bool IsTheBoardFull()
        {
            return _tokenPositions.All(token => token != Token.None);
        }

        public bool IsThereAWinningStripe(Token currentToken)
        {
            return WinningStripes
                .Any(stripe =>
                    stripe.All(position => _tokenPositions[(int)position] == currentToken));
        }

        private bool CanPlaceToken(Token token, BoardPosition boardPosition)
        {
            var lastPlayedToken = GetLastPlayedToken();

            if (lastPlayedToken == token)
                return false;

            if (!IsPositionAvailable(boardPosition))
                return false;

            return true;
        }

        private Token GetLastPlayedToken()
        {
            var numberOfPlays = _tokenPositions.Count(x => x != Token.None);
            return (Token)(numberOfPlays % 2 + 1);
        }

        private bool IsPositionAvailable(BoardPosition currentPosition)
        {
            return _tokenPositions[(int)currentPosition] == Token.None;
        }
    }
}
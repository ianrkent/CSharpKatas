namespace Katas.CSharp.Tests.TicTacToe
{
    public class TicTakToeGame
    {
        private readonly TicTakToeBoard _board = new TicTakToeBoard();

        public MoveResult MakeAPlay(Token token, BoardPosition boardPosition)
        {
            if (!_board.TryPlaceToken(token, boardPosition))
                return MoveResult.InvalidMove;

            if (_board.IsThereAWinningStripe(token))
                return MoveResult.YouAreAWinner;

            if (_board.IsTheBoardFull())
                return MoveResult.NoWinnerYouDrawers;

            return MoveResult.InProgress; 
        }
    }
}
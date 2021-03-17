using Core.Exceptions;
using Core.Model;

namespace Core.Services
{
    public interface IGame
    {
        Board StartGame();
        bool IsFinished(Board board);
        
        /// <exception cref="CannotMakeOutOfBoundsShotException">Board does not contain given Cell</exception>
        /// <exception cref="CannotShotAlreadyShotCellException">Cell has already been shot</exception>
        GameMoveResult ShootAt(Board board, Cell cell);
    }
}
using Core.Model;

namespace Core.Services
{
    public interface IGame
    {
        Board StartGame();
        void EndGame();
        bool IsFinished(Board board);
        GameMoveResult ShootAt(Board board, Cell cell);
    }
}
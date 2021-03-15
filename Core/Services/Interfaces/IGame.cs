using Core.Model;

namespace Core.Services
{
    public interface IGame
    {
        Board StartGame();
        void EndGame();
        bool IsFinished();
        GameMoveResult ShootAt(Cell cell);
    }
}
using Core.Model;

namespace Core.Services
{
    public sealed class Game : IGame
    {
        public Board StartGame() => throw new System.NotImplementedException();


        public void EndGame() => throw new System.NotImplementedException();


        public bool IsFinished() => throw new System.NotImplementedException();


        public GameMoveResult ShootAt(Cell cell) => throw new System.NotImplementedException();
    }
}
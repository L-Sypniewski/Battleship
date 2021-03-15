using System.Collections.Generic;
using Core.Model;

namespace Core.Services
{
    public sealed class Game : IGame
    {
        private readonly BoardSize _boardSize;
        private readonly ISet<ShipConfiguration> _shipConfigurations;
        private readonly IBoardInitializer _boardInitializer;


        public Game(BoardSize boardSize,
                    ISet<ShipConfiguration> shipConfigurations,
                    IBoardInitializer boardInitializer)
        {
            _boardSize = boardSize;
            _shipConfigurations = shipConfigurations;
            _boardInitializer = boardInitializer;
        }


        public Board StartGame()
        {
            return _boardInitializer.InitializedBoard(_boardSize, _shipConfigurations);
        }


        public void EndGame() => throw new System.NotImplementedException();


        public bool IsFinished() => throw new System.NotImplementedException();


        public GameMoveResult ShootAt(Cell cell) => throw new System.NotImplementedException();
    }
}
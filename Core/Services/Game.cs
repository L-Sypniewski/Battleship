using System.Collections.Generic;
using System.Linq;
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


        public bool IsFinished(Board board) => board.Ships
                                                    .SelectMany(ship => ship.Cells)
                                                    .All(cell => cell.IsShot);


        public GameMoveResult ShootAt(Board board, Cell cell) => throw new System.NotImplementedException();
    }
}
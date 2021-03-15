using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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


        public GameMoveResult ShootAt(Board board, Cell cell)
        {
            var allShips = board.Ships;

            var shipWihCellToShot = ShipContaining(allShips, cell);
            var cellsFromShipToShot = shipWihCellToShot.Cells.ToList();
            var indexOfCellToRemove = cellsFromShipToShot.IndexOf(cell);

            var isCellRemoved = cellsFromShipToShot.Remove(cell);
            if (!isCellRemoved)
            {
                throw new ApplicationException("Cell was not removed");
            }

            cellsFromShipToShot.Insert(indexOfCellToRemove, cell with {IsShot = true});
            var updatedShip = shipWihCellToShot with {Cells = cellsFromShipToShot.ToImmutableArray()};

            var indexOfShipToUpdate = allShips.IndexOf(shipWihCellToShot);
            var shipsWithRemovedShipToUpdate = allShips.Remove(shipWihCellToShot);
            var updatedShips = shipsWithRemovedShipToUpdate.Insert(indexOfShipToUpdate, updatedShip);

            var updatedBoard = board with {Ships = updatedShips};

            return new GameMoveResult(updatedBoard, updatedShip);
        }


        private static Ship ShipContaining(IEnumerable<Ship> ships, Cell cell)
        {
            return ships.Single(ship => ship.Cells.Contains(cell));
        }
    }
}
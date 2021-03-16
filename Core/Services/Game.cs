using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Core.Exceptions;
using Core.Model;

namespace Core.Services
{
    public sealed class Game : IGame
    {
        private readonly IBoardInitializer _boardInitializer;
        private readonly BoardSize _boardSize;
        private readonly IBoardVerifier _boardVerifier;
        private readonly ISet<ShipConfiguration> _shipConfigurations;


        public Game(BoardSize boardSize, ISet<ShipConfiguration> shipConfigurations,
                    IBoardInitializer boardInitializer, IBoardVerifier boardVerifier)
        {
            _boardSize = boardSize;
            _shipConfigurations = shipConfigurations;
            _boardInitializer = boardInitializer;
            _boardVerifier = boardVerifier;
        }


        public Board StartGame()
        {
            return _boardInitializer.InitializedBoard(_boardSize, _shipConfigurations);
        }


        public void EndGame() => throw new NotImplementedException();


        public bool IsFinished(Board board) => board.Ships.All(ship => ship.IsSunk);


        public GameMoveResult ShootAt(Board board, Cell cell)
        {
            var cellIsOutOfBounds = !_boardVerifier.CellsAreWithinBounds(board.Size, new[] {cell});
            if (cellIsOutOfBounds)
            {
                throw new CannotMakeOutOfBoundsShotException();
            }

            var shotShip = ShotShip(board, cell);
            var updatedBoard = UpdatedBoard(board, cell);
            return new GameMoveResult(updatedBoard, shotShip);
        }


        private static Ship? ShotShip(Board oldBoard, Cell cellToShot)
        {
            var shipWithCellToShot = ShipContaining(oldBoard.Ships, cellToShot);
            var updatedCells = UpdatedCellsFrom(shipWithCellToShot, cellToShot);
            return shipWithCellToShot with {Cells = updatedCells};
        }


        private static Board UpdatedBoard(Board oldBoard, Cell cellToShot)
        {
            var allShips = oldBoard.Ships;

            var shipWithCellToShot = ShipContaining(allShips, cellToShot);
            var updatedCells = UpdatedCellsFrom(shipWithCellToShot, cellToShot);
            var updatedShips = UpdatedShips(shipWithCellToShot, allShips, updatedCells);
            return oldBoard with {Ships = updatedShips};
        }


        private static IImmutableList<Ship> UpdatedShips(Ship shipToBeUpdated,
                                                         IImmutableList<Ship> allShips,
                                                         IImmutableList<Cell> updatedCells)
        {
            var updatedShip = shipToBeUpdated with {Cells = updatedCells};

            var indexOfShipToUpdate = allShips.IndexOf(shipToBeUpdated);
            var shipsWithRemovedShipToUpdate = allShips.Remove(shipToBeUpdated);
            return shipsWithRemovedShipToUpdate.Insert(indexOfShipToUpdate, updatedShip);
        }


        private static IImmutableList<Cell> UpdatedCellsFrom(Ship shipWithCellToShot,
                                                             Cell cellToShot)
        {
            var cellsFromShipToShot = shipWithCellToShot.Cells;

            var indexOfCellToRemove = cellsFromShipToShot.IndexOf(cellToShot);

            var cellToShotAlreadyShot = cellsFromShipToShot[indexOfCellToRemove].IsShot;
            if (cellToShotAlreadyShot)
            {
                throw new CannotShotAlreadyShotCellException();
            }

            var cellsWithRemovedCellToShoot = cellsFromShipToShot.Remove(cellToShot);

            return cellsWithRemovedCellToShoot.Insert(indexOfCellToRemove, cellToShot with {IsShot = true});
        }


        private static Ship ShipContaining(IEnumerable<Ship> ships, Cell cell)
        {
            return ships.Single(ship => ship.Cells.Contains(cell));
        }
    }
}
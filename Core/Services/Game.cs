using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Core.Exceptions;
using Core.Model;
using Core.Utils;

namespace Core.Services
{
    public sealed class Game : IGame
    {
        private readonly IBoardInitializer _boardInitializer;
        private readonly BoardSize _boardSize;
        private readonly IBoardVerifier _boardVerifier;
        private readonly ISet<ShipConfiguration> _shipConfigurations;

        private static readonly IEqualityComparer<Cell> _cellEqualityComparer = new CellIgnoringIsShotComparer();


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


        public bool IsFinished(Board board) => board.Ships.All(ship => ship.IsSunk);


        public GameMoveResult ShootAt(Board board, Cell cell)
        {
            var cellIsOutOfBounds = !_boardVerifier.CellsAreWithinBounds(board.Size, new[] {cell});
            if (cellIsOutOfBounds)
            {
                throw new CannotMakeOutOfBoundsShotException();
            }

            var shotShip = ShotShip(board, cell);
            var updatedBoard = UpdatedBoard(board, cell, shotShip);
            return new GameMoveResult(updatedBoard, shotShip);
        }


        private static Ship? ShotShip(Board oldBoard, Cell cellToShot)
        {
            var shipWithCellToShot = ShipContaining(oldBoard.Ships, cellToShot);
            if (shipWithCellToShot is null)
            {
                return null;
            }

            var updatedCells = UpdatedCellsFrom(shipWithCellToShot, cellToShot);
            return shipWithCellToShot with {Cells = updatedCells};
        }


        private static Board UpdatedBoard(Board oldBoard, Cell cellToShot, Ship? shotShip)
        {
            if (shotShip is not null)
            {
                var allShips = oldBoard.Ships;

                var updatedShips = UpdatedShips(shotShip, allShips);
                return oldBoard with {Ships = updatedShips};
            }

            var updatedCellsWithoutShips = UpdatedCellsWithoutShips(oldBoard, cellToShot);
            return oldBoard with {CellsWithoutShips = updatedCellsWithoutShips};
        }


        private static IImmutableList<Cell> UpdatedCellsWithoutShips(Board oldBoard, Cell cellToShot)
        {
            var oldCells = oldBoard.CellsWithoutShips;
            var cellToUpdate = oldCells.Single(cell => new CellIgnoringIsShotComparer().Equals(cell, cellToShot));

            if (cellToUpdate.IsShot)
            {
                throw new CannotShotAlreadyShotCellException();
            }

            return oldCells.WithReplaced(cellToUpdate with {IsShot = true}, cell => cell == cellToShot);
        }


        private static IImmutableList<Ship> UpdatedShips(Ship updatedShip, IImmutableList<Ship> allShips)
        {
            return allShips.WithReplaced(updatedShip,
                                         ship => ship.Cells.SequenceEqual(updatedShip.Cells, _cellEqualityComparer));
        }


        private static IImmutableList<Cell> UpdatedCellsFrom(Ship shipWithCellToShot,
                                                             Cell cellToShot)
        {
            var cellsFromShipToShot = shipWithCellToShot.Cells;

            var indexOfCellToRemove = cellsFromShipToShot.IndexOf(cellToShot, _cellEqualityComparer);

            var cellToShotAlreadyShot = cellsFromShipToShot[indexOfCellToRemove].IsShot;
            if (cellToShotAlreadyShot)
            {
                throw new CannotShotAlreadyShotCellException();
            }

            var cellsWithRemovedCellToShoot = cellsFromShipToShot.Remove(cellToShot);

            return cellsWithRemovedCellToShoot.Insert(indexOfCellToRemove, cellToShot with {IsShot = true});
        }


        private static Ship? ShipContaining(IEnumerable<Ship> ships, Cell cell)
        {
            return ships.SingleOrDefault(ship => ship.Cells.Contains(cell, _cellEqualityComparer));
        }
    }
}
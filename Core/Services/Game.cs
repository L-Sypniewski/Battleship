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
        private static readonly IEqualityComparer<Cell> _cellEqualityComparer = new CellIgnoringIsShotComparer();
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


        public Board StartGame() => _boardInitializer.InitializedBoard(_boardSize, _shipConfigurations);


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
            var shipWithCellToShot = ShipContaining(cellToShot, oldBoard.Ships);

            if (shipWithCellToShot is null)
            {
                return null;
            }

            if (shipWithCellToShot.IsSunk)
            {
                throw new CannotShotAlreadyShotCellException();
            }

            var updatedCells = UpdatedCellsFrom(shipWithCellToShot, cellToShot);
            return shipWithCellToShot with {Cells = updatedCells};
        }


        private static Board UpdatedBoardIfShipWasShot(Board oldBoard, Ship shotShip)
        {
            var allShips = oldBoard.Ships;

            var updatedShips = UpdatedShips(shotShip, allShips);
            return oldBoard with {Ships = updatedShips};
        }


        private static Board UpdatedBoard(Board oldBoard, Cell cellToShot, Ship? shotShip)
        {
            return shotShip == null
                ? UpdatedBoardIfNoShipShot(oldBoard, cellToShot)
                : UpdatedBoardIfShipWasShot(oldBoard, shotShip);
        }


        private static Board UpdatedBoardIfNoShipShot(Board oldBoard, Cell cellToShot)
        {
            var oldCells = oldBoard.CellsWithoutShips;
            var cellToUpdate = oldCells.Single(cell => _cellEqualityComparer.Equals(cell, cellToShot));

            if (cellToUpdate.IsShot)
            {
                throw new CannotShotAlreadyShotCellException();
            }

            var updatedCells = oldCells.WithReplaced(cellToUpdate with {IsShot = true}, cell => cell == cellToShot);

            return oldBoard with {CellsWithoutShips = updatedCells};
        }


        private static IImmutableList<Ship> UpdatedShips(Ship updatedShip, IImmutableList<Ship> allShips)
        {
            return allShips.WithReplaced(updatedShip,
                                         ship => ship.Cells.SequenceEqual(updatedShip.Cells, _cellEqualityComparer));
        }


        private static IImmutableList<Cell> UpdatedCellsFrom(Ship shipWithCellToShot, Cell cellToShot)
        {
            var cellsFromShipToShot = shipWithCellToShot.Cells;

            var indexOfCellToUpdate = cellsFromShipToShot.IndexOf(cellToShot, _cellEqualityComparer);

            var cellToShotAlreadyShot = cellsFromShipToShot[indexOfCellToUpdate].IsShot;
            if (cellToShotAlreadyShot)
            {
                throw new CannotShotAlreadyShotCellException();
            }

            return cellsFromShipToShot.WithReplaced(cellToShot with {IsShot = true},
                                                    cell => _cellEqualityComparer.Equals(cell, cellToShot));
        }


        private static Ship? ShipContaining(Cell cell, IEnumerable<Ship> ships)
        {
            return ships.SingleOrDefault(ship => ship.Cells.Contains(cell, _cellEqualityComparer));
        }
    }
}
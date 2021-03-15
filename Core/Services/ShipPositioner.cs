using System;
using System.Collections.Generic;
using System.Linq;
using Core.Exceptions;
using Core.Model;

namespace Core.Services
{
    public sealed class ShipPositioner : IShipPositioner
    {
        private readonly IBoardVerifier _boardVerifier;
        private readonly ICellRandomizer _cellRandomizer;
        private readonly int _maxAttempts;
        private readonly IShipOrientationRandomizer _shipOrientationRandomizer;


        public ShipPositioner(int maxAttempts, IShipOrientationRandomizer shipOrientationRandomizer,
                              ICellRandomizer cellRandomizer, IBoardVerifier boardVerifier)
        {
            _maxAttempts = Math.Max(1, maxAttempts);
            _shipOrientationRandomizer = shipOrientationRandomizer;
            _cellRandomizer = cellRandomizer;
            _boardVerifier = boardVerifier;
        }


        public IReadOnlyCollection<Cell> ShipPositionsFor(Board board, int shipSize)
        {
            var counter = 0;
            do
            {
                var firstCell = _cellRandomizer.GetCellWithin(board.Size);
                var orientation = _shipOrientationRandomizer.GetOrientation();

                var shipPositions = PositionsFor(orientation, firstCell, shipSize);

                var cellsAreWithinBoardBounds = _boardVerifier.CellsAreWithinBounds(board.Size, shipPositions);
                if (cellsAreWithinBoardBounds)
                {
                    return shipPositions;
                }

                counter++;
            } while (counter <= _maxAttempts);

            throw new CannotCreateShipPositionsException();
        }


        private static Cell[] PositionsFor(ShipOrientation orientation, Cell firstCell, int shipSize)
        {
            if (orientation == ShipOrientation.Horizontal)
            {
                var xCoordinates = Enumerable.Range(firstCell.XCoordinate, shipSize);
                return xCoordinates.Select(x => new Cell(x, firstCell.YCoordinate, false)).ToArray();
            }

            var yCoordinates = Enumerable.Range(firstCell.YCoordinate, shipSize);
            return yCoordinates.Select(y => new Cell(firstCell.XCoordinate, y, false)).ToArray();
        }
    }
}
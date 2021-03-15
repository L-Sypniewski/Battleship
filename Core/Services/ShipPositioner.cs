using System.Collections.Generic;
using System.Linq;
using Core.Model;

namespace Core.Services
{
    public sealed class ShipPositioner : IShipPositioner
    {
        private readonly IShipOrientationRandomizer _shipOrientationRandomizer;
        private readonly ICellRandomizer _cellRandomizer;
        private readonly IBoardVerifier _boardVerifier;


        public ShipPositioner(IShipOrientationRandomizer shipOrientationRandomizer,
                              ICellRandomizer cellRandomizer,
                              IBoardVerifier boardVerifier)
        {
            _shipOrientationRandomizer = shipOrientationRandomizer;
            _cellRandomizer = cellRandomizer;
            _boardVerifier = boardVerifier;
        }


        public IReadOnlyCollection<Cell> ShipPositionsFor(Board board, int shipSize)
        {
            var firstCell = _cellRandomizer.GetCellWithin(board.Size);
            var orientation = _shipOrientationRandomizer.GetOrientation();

            var shipPositions = PositionsFor(orientation, firstCell, shipSize);
            return shipPositions;
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
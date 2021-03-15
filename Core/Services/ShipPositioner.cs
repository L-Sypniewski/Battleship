using System.Collections.Generic;
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
            var shipPositions = new List<Cell>(shipSize);

            if (orientation == ShipOrientation.Horizontal)
            {
                for (var i = firstCell.XCoordinate; i < firstCell.XCoordinate + shipSize; i++)
                {
                    shipPositions.Add(new Cell(i, firstCell.YCoordinate, false));
                }

                return shipPositions;
            }

            for (var i = firstCell.YCoordinate; i < firstCell.YCoordinate + shipSize; i++)
            {
                shipPositions.Add(new Cell(firstCell.XCoordinate, i, false));
            }

            return shipPositions;
        }
    }
}
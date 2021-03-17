using System.Collections.Generic;
using System.Linq;
using Core.Exceptions;
using Core.Model;

namespace Core.Services
{
    public sealed class BoardVerifier : IBoardVerifier
    {
        public bool CellsAreWithinBounds(BoardSize boardSize, IEnumerable<Cell> cells)
        {
            var cellsArray = cells.ToArray();

            var cellsWithNegativeCoordinates = cellsArray.Where(CellsContainsNegativeCoordinate).ToArray();
            if (cellsWithNegativeCoordinates.Any())
            {
                var errorMessage = string.Join(',', cellsWithNegativeCoordinates.Select(ship => ship.ToString()));
                throw new CellsNegativeCoordinatesException(errorMessage);
            }

            var cellsWithXCoordinatesOutOfBounds = cellsArray.Where(cell => cell.XCoordinate >= boardSize.XSize);
            var cellsWithYCoordinatesOutOfBounds = cellsArray.Where(cell => cell.YCoordinate >= boardSize.YSize);

            var outOfBoundsShips = cellsWithXCoordinatesOutOfBounds.Union(cellsWithYCoordinatesOutOfBounds).ToArray();

            return !outOfBoundsShips.Any();
        }


        private static bool CellsContainsNegativeCoordinate(Cell cell)
        {
            return cell.XCoordinate < 0 || cell.YCoordinate < 0;
        }
    }
}
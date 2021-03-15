using System.Collections.Generic;
using System.Linq;
using Core.Exceptions;
using Core.Model;

namespace Core.Services
{
    public sealed class BoardVerifier : IBoardVerifier
    {
        public bool ShipsIntersectEachOther(Board board)
        {
            var cellsOfAllShips = board.Ships.SelectMany(ship => ship.Cells).ToArray();
            var shipsIntersectEachOther = cellsOfAllShips.Distinct().Count() != cellsOfAllShips.Length;
            return shipsIntersectEachOther;
        }


        public bool CellsAreWithinBounds(BoardSize boardSize, IEnumerable<Cell> cells)
        {
            var cellsWithNegativeCoordinates = cells.Where(CellsContainsNegativeCoordinate).ToArray();
            if (cellsWithNegativeCoordinates.Any())
            {
                var errorMessage = string.Join(',', cellsWithNegativeCoordinates.Select(ship => ship.ToString()));
                throw new CellsNegativeCoordinatesException(errorMessage);
            }

            var cellsWithXCoordinatesOutOfBounds = cells.Where(cell => ShipHasCellWithXCoordinateOutOfBoundsOf(boardSize, cell));
            var cellsWithYCoordinatesOutOfBounds = cells.Where(cell => ShipHasCellWithYCoordinateOutOfBoundsOf(boardSize, cell));

            var outOfBoundsShips = cellsWithXCoordinatesOutOfBounds.Union(cellsWithYCoordinatesOutOfBounds).ToArray();

            return !outOfBoundsShips.Any();
        }


        private static bool ShipHasCellWithXCoordinateOutOfBoundsOf(BoardSize boardSize, Cell cell)
        {
            return cell.XCoordinate > boardSize.XSize;
        }


        private static bool ShipHasCellWithYCoordinateOutOfBoundsOf(BoardSize boardSize, Cell cell)
        {
            return cell.YCoordinate > boardSize.YSize;
        }


        private static bool CellsContainsNegativeCoordinate(Cell cell)
        {
            return cell.XCoordinate < 0 || cell.YCoordinate < 0;
        }
    }
}
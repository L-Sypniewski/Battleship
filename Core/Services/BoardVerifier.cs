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
            var cellsArray = cells.ToArray();

            var cellsWithNegativeCoordinates = cellsArray.Where(CellsContainsNegativeCoordinate).ToArray();
            if (cellsWithNegativeCoordinates.Any())
            {
                var errorMessage = string.Join(',', cellsWithNegativeCoordinates.Select(ship => ship.ToString()));
                throw new CellsNegativeCoordinatesException(errorMessage);
            }

            var cellsWithXCoordinatesOutOfBounds = cellsArray.Where(cell => CellHasXCoordinateOutOfBoundsOf(boardSize, cell));
            var cellsWithYCoordinatesOutOfBounds = cellsArray.Where(cell => CellHasYCoordinateOutOfBoundsOf(boardSize, cell));

            var outOfBoundsShips = cellsWithXCoordinatesOutOfBounds.Union(cellsWithYCoordinatesOutOfBounds).ToArray();

            return !outOfBoundsShips.Any();
        }


        private static bool CellHasXCoordinateOutOfBoundsOf(BoardSize boardSize, Cell cell)
        {
            return cell.XCoordinate > boardSize.XSize;
        }


        private static bool CellHasYCoordinateOutOfBoundsOf(BoardSize boardSize, Cell cell)
        {
            return cell.YCoordinate > boardSize.YSize;
        }


        private static bool CellsContainsNegativeCoordinate(Cell cell)
        {
            return cell.XCoordinate < 0 || cell.YCoordinate < 0;
        }
    }
}
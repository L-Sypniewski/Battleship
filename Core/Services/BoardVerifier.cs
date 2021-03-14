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


        public bool ShipsAreWithinBounds(Board board)
        {
            var (boardSize, ships) = board;

            var shipsWithNegativeCoordinates = ships.Where(ShipHasCellWithNegativeCoordinates).ToArray();
            if (shipsWithNegativeCoordinates.Any())
            {
                var errorMessage = string.Join(',', shipsWithNegativeCoordinates.Select(ship => ship.ToString()));
                throw new ShipsNegativeCoordinatesException(errorMessage);
            }

            var shipsWithXCoordinatesOutOfBounds = ships.Where(ship => ShipHasCellWithXCoordinateOutOfBoundsOf(boardSize, ship));
            var shipsWithYCoordinatesOutOfBounds = ships.Where(ship => ShipHasCellWithYCoordinateOutOfBoundsOf(boardSize, ship));

            var outOfBoundsShips = shipsWithXCoordinatesOutOfBounds.Union(shipsWithYCoordinatesOutOfBounds).ToArray();

            return !outOfBoundsShips.Any();
        }


        private static bool ShipHasCellWithXCoordinateOutOfBoundsOf(BoardSize boardSize, Ship ship)
        {
            return ship.Cells.Any(cell => cell.XCoordinate > boardSize.XSize);
        }


        private static bool ShipHasCellWithYCoordinateOutOfBoundsOf(BoardSize boardSize, Ship ship)
        {
            return ship.Cells.Any(cell => cell.YCoordinate > boardSize.YSize);
        }


        private static bool ShipHasCellWithNegativeCoordinates(Ship ship)
        {
            return ship.Cells.Any(cell => cell.XCoordinate < 0
                                          || cell.YCoordinate < 0);
        }
    }
}
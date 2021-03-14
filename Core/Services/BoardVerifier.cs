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

            var shipsWithNegativeCoordinates = ships.Where(ship => ship.Cells.Any(cell => cell.XCoordinate < 0
                                                                                      || cell.YCoordinate < 0))
                                                    .ToArray();
            if (shipsWithNegativeCoordinates.Any())
            {
                var message = string.Join(',', shipsWithNegativeCoordinates.Select(ship => ship.ToString()));
                throw new ShipsNegativeCoordinatesException(
                    message);
            }

            var shipsWithXCoordinatesOutOfBounds = ships
                                                   .Where(ship => ship.Cells.Any(cell => cell.XCoordinate > boardSize.XSize))
                                                   .ToArray();
            var shipsWithYCoordinatesOutOfBounds = ships
                                                   .Where(ship => ship.Cells.Any(cell => cell.YCoordinate > boardSize.YSize))
                                                   .ToArray();
            var outOfBoundsShips = shipsWithXCoordinatesOutOfBounds.Union(shipsWithYCoordinatesOutOfBounds).ToArray();

            return !outOfBoundsShips.Any();
        }
    }
}
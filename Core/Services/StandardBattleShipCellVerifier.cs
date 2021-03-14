using System.Collections.Generic;
using System.Linq;
using Core.Model;

namespace Core.Services
{
    /// <summary>
    ///     <see cref="ICellVerifier" /> for a standard variant of Battleships game.
    ///     It checks if all <see cref="Cell" />s are adjacent and placed on the same axis
    /// </summary>
    public sealed class StandardBattleShipCellVerifier : ICellVerifier
    {
        public bool Verify(IEnumerable<Cell> cells)
        {
            var cellsArray = cells.ToArray();

            var allCellsAreOnXAxis = cellsArray.Select(cell => cell.XCoordinate).Distinct().Count() == 1;
            var allCellsAreOnYAxis = cellsArray.Select(cell => cell.YCoordinate).Distinct().Count() == 1;

            if (!allCellsAreOnXAxis && !allCellsAreOnYAxis) return false;

            if (allCellsAreOnXAxis)
            {
                var allYCoordinates = cellsArray.Select(cell => cell.YCoordinate).OrderBy(y => y).ToArray();
                var yCoordinatesCount = allYCoordinates.Length;
                var areAdjacent = allYCoordinates.Last() - allYCoordinates.First() == yCoordinatesCount - 1;
                return areAdjacent;
            }

            var allXCoordinates = cellsArray.Select(cell => cell.XCoordinate).OrderBy(x => x).ToArray();
            var xCoordinatesCount = allXCoordinates.Length;
            return allXCoordinates.Last() - allXCoordinates.First() == xCoordinatesCount - 1;
        }
    }
}
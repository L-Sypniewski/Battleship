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

            return allCellsAreOnXAxis
                ? AreAllYCoordinatesAdjacent(cellsArray)
                : AreAllXCoordinatesAdjacent(cellsArray);
        }


        private static bool AreIntegersAdjacent(IEnumerable<int> integers)
        {
            var sortedIntegers = integers.OrderBy(i => i).ToArray();
            return sortedIntegers.Last() - sortedIntegers.First() == sortedIntegers.Length - 1;
        }


        private static bool AreAllYCoordinatesAdjacent(IEnumerable<Cell> cells)
        {
            var allYCoordinates = cells.Select(cell => cell.YCoordinate);
            return AreIntegersAdjacent(allYCoordinates);
        }


        private static bool AreAllXCoordinatesAdjacent(IEnumerable<Cell> cells)
        {
            var allXCoordinates = cells.Select(cell => cell.XCoordinate);
            return AreIntegersAdjacent(allXCoordinates);
        }
    }
}
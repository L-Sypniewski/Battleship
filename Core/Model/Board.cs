using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public sealed record Board(BoardSize Size, IImmutableList<Ship> Ships,
                               IImmutableList<Cell> CellsWithoutShips)
    {
        public bool Equals(Board? other) => Size.Equals(other?.Size) &&
                                            Ships.SequenceEqual(other.Ships) &&
                                            CellsWithoutShips.SequenceEqual(other.CellsWithoutShips);


        private bool PrintMembers(StringBuilder builder)
        {
            builder.Append($"{nameof(BoardSize)}: {Size},");
            builder.Append($"{nameof(Ships)}: {string.Join(',', Ships.Select(ship => ship.ToString()))}");
            builder.Append($"{nameof(CellsWithoutShips)}: {string.Join(',', CellsWithoutShips.Select(ship => ship.ToString()))}");
            return true;
        }


        public override int GetHashCode() => HashCode.Combine(Size, Ships, CellsWithoutShips);


        public CellState CellState(Cell cellToCheck)
        {
            var doesCellBelongsToShip = DoesCellBelongToShip(Ships, cellToCheck);
            if (!doesCellBelongsToShip)
            {
                var cellWithoutShip = CellsWithoutShips.SingleOrDefault(cell => CompareCellsWithoutIsShot(cell, cellToCheck));
                if (cellWithoutShip is null)
                {
                    return Model.CellState.Clear;
                }

                return cellWithoutShip.IsShot ? Model.CellState.Miss : Model.CellState.Clear;
            }

            var allShipCells = Ships.SelectMany(ship => ship.Cells).ToArray();
            var cellFromBoard = allShipCells.Single(cell => CompareCellsWithoutIsShot(cell, cellToCheck));
            if (!cellFromBoard.IsShot)
            {
                return Model.CellState.Clear;
            }

            var shipForACell =
                Ships.Single(ship => ship.Cells.SingleOrDefault(cell => CompareCellsWithoutIsShot(cell, cellToCheck)) != null);

            return shipForACell.IsSunk ? Model.CellState.Sunk : Model.CellState.Hit;
        }


        private static bool DoesCellBelongToShip(IImmutableList<Ship> ships, Cell cellToCheck)
        {
            var allShipCells = ships.SelectMany(ship => ship.Cells).ToArray();
            return allShipCells.Any(cell => CompareCellsWithoutIsShot(cell, cellToCheck));
        }


        private static bool CompareCellsWithoutIsShot(Cell lhs, Cell rhs) => lhs.XCoordinate == rhs.XCoordinate &&
                                                                             lhs.YCoordinate == rhs.YCoordinate;
    }
}
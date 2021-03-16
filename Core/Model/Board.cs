using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public sealed record Board(BoardSize Size, IImmutableList<Ship> Ships)
    {
        public bool Equals(Board? other) => Size.Equals(other?.Size) &&
                                            Ships.SequenceEqual(other.Ships);


        private bool PrintMembers(StringBuilder builder)
        {
            builder.Append($"{nameof(BoardSize)}: {Size},");
            builder.Append($"{nameof(Ships)}: {string.Join(',', Ships.Select(ship => ship.ToString()))}");
            return true;
        }


        public override int GetHashCode() => HashCode.Combine(Size, Ships);


        public CellState CellState(Cell cellToCheck)
        {
            var allCells = Ships.SelectMany(ship => ship.Cells).ToArray();
            
            var doesCellBelongsToShip = allCells.Contains(cellToCheck);
            if (!doesCellBelongsToShip)
            {
                return Model.CellState.Clear;
            }

            var cellFromBoard = allCells.Single(cell => cell == cellToCheck);
            if (!cellFromBoard.IsShot)
            {
                return Model.CellState.Clear;
            }

            var shipForACell = Ships.Single(ship => ship.Cells.Contains(cellToCheck));

            return shipForACell.IsSunk ? Model.CellState.Sunk : Model.CellState.Hit;
        }
    }
}
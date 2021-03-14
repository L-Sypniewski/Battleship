using System.Linq;
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
    }
}
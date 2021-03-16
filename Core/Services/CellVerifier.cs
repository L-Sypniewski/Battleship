using System.Collections.Generic;
using System.Linq;
using Core.Model;

namespace Core.Services
{
    public sealed class CellVerifier : ICellVerifier
    {
        public bool CellsIntersect(IEnumerable<Cell> cells)
        {
            var cellsArray = cells.ToArray();
            var equalityComparer = new CellIgnoringIsShotComparer();
            var cellsIntersect = cellsArray.Distinct(equalityComparer).Count() != cellsArray.Length;
            return cellsIntersect;
        }
    }
}
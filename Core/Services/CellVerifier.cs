using System;
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
            var equalityComparer = new CellComparerIgnoringIsShot();
            var cellsIntersect = cellsArray.Distinct(equalityComparer).Count() != cellsArray.Length;
            return cellsIntersect;
        }


        private sealed class CellComparerIgnoringIsShot : IEqualityComparer<Cell>
        {
            public bool Equals(Cell? x, Cell? y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (x?.GetType() != y?.GetType()) return false;
                return x?.XCoordinate == y?.XCoordinate && x?.YCoordinate == y?.YCoordinate;
            }


            public int GetHashCode(Cell obj)
            {
                return HashCode.Combine(obj.XCoordinate, obj.YCoordinate);
            }
        }
    }
}
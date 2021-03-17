using System;
using System.Collections.Generic;

namespace Core.Model
{
    public sealed class CellIgnoringIsShotComparer : IEqualityComparer<Cell>
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
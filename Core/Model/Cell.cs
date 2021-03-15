using System;

namespace Core.Model
{
    public sealed record Cell(int XCoordinate, int YCoordinate, bool IsShot)
    {
        public override int GetHashCode() => HashCode.Combine(XCoordinate, YCoordinate);


        public bool Equals(Cell? other) => XCoordinate.Equals(other?.XCoordinate) && YCoordinate.Equals(other.YCoordinate);
    }
}
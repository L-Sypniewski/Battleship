using System;
using Core.Services;

namespace Core.Model
{
    /// <summary>
    ///     Ship configuration used by <see cref="IBoardInitializer" /> to initialize a board.
    ///     Two configuraions for the same <see cref="Name" /> and <see cref="ShipSize" /> are considered equal
    /// </summary>
    public sealed record ShipConfiguration(string Name, int ShipSize, int ShipsNumber)
    {
        public bool Equals(ShipConfiguration? other) => ShipSize.Equals(other?.ShipSize) && Name.Equals(other.Name);
        public override int GetHashCode() => HashCode.Combine(Name, ShipSize);
    }
}
using Core.Services;

namespace Core.Model
{
    /// <summary>
    ///     Ship configuration used by <see cref="IBoardInitializer" /> to initialize a board.
    ///     Two configuraions for the same <see cref="ShipSize" /> are consifered equal
    /// </summary>
    public sealed record ShipConfiguration(int ShipSize, int ShipsNumber)
    {
        public override int GetHashCode() => ShipSize.GetHashCode();

        public bool Equals(ShipConfiguration? other) => ShipSize.Equals(other?.ShipSize);

    }
}
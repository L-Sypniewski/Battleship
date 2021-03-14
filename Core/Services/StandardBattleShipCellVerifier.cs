using System.Collections.Generic;
using Core.Model;

namespace Core.Services
{
    /// <summary>
    /// <see cref="ICellVerifier"/> for a standard variant of Battleships game.
    /// It checks if all <see cref="Cell"/>s are adjacent and placed on the same axis
    /// </summary>
    public sealed class StandardBattleShipCellVerifier : ICellVerifier
    {
        public bool Verify(IEnumerable<Cell> cells) => throw new System.NotImplementedException();
    }
}
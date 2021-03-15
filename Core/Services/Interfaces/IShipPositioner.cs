using System.Collections.Generic;
using Core.Model;

namespace Core.Services
{
    public interface IShipPositioner
    {
        IReadOnlyCollection<Cell> ShipPositionsFor(Board board, int shipSize);
    }
}
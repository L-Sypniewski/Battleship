using System.Collections.Generic;
using Core.Exceptions;
using Core.Model;

namespace Core.Services
{
    public interface IShipPositioner
    {
        /// <exception cref="CannotCreateShipPositionsException"></exception>
        IReadOnlyCollection<Cell> ShipPositionsFor(Board board, int shipSize);
    }
}
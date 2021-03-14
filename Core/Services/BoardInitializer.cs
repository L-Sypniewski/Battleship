using System;
using System.Collections.Generic;
using System.Linq;
using Core.Model;

namespace Core.Services
{
    public sealed class BoardInitializer : IBoardInitializer
    {
        public Board InitializedBoard(BoardSize boardSize, ISet<ShipConfiguration> shipConfigurations)
        {
            var shipsNumber = shipConfigurations.Sum(x => x.ShipsNumber);

            var emptyShip = new Ship("", Array.Empty<Cell>());
            return new Board(boardSize, Enumerable.Repeat(emptyShip, shipsNumber).ToArray());
        }
    }
}
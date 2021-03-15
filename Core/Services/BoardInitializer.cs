using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Core.Model;

namespace Core.Services
{
    public sealed class BoardInitializer : IBoardInitializer
    {
        private readonly IShipPositioner _shipPositioner;
        private readonly ICellVerifier _cellVerifier;


        public BoardInitializer(IShipPositioner shipPositioner, ICellVerifier cellVerifier)
        {
            _shipPositioner = shipPositioner;
            _cellVerifier = cellVerifier;
        }


        public Board InitializedBoard(BoardSize boardSize, ISet<ShipConfiguration> shipConfigurations)
        {
            var shipsNumber = shipConfigurations.Sum(x => x.ShipsNumber);

            var board = new Board(boardSize, new ImmutableArray<Ship>());

            /*
            var shipsWithoutPosition = shipConfigurations.SelectMany(config =>
            {
                for (var i = 0; i < config.ShipsNumber; i++)
                {
                    do
                    {
                        var newShipPosition = _shipPositioner.ShipPositionsFor(board, config.ShipSize).ToImmutableArray();
                        var newShip = new Ship(config.Name, newShipPosition);

                        var currentShips = board.Ships;
                        var updatedShips = currentShips.Add(newShip);
                        var boardWithNewShip = board with {Ships = updatedShips};

                        var doShipsIntersectEachOther = _boardVerifier.ShipsIntersectEachOther(boardWithNewShip);
                        if (!doShipsIntersectEachOther)
                        {
                            return 
                        }
                    } while (true);
                }
            }).ToArray();
            */


            var emptyShip = new Ship("", Enumerable.Empty<Cell>().ToImmutableList());
            return new Board(boardSize, Enumerable.Repeat(emptyShip, shipsNumber).ToImmutableList());
        }
    }
}
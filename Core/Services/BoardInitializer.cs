using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Core.Exceptions;
using Core.Model;

namespace Core.Services
{
    public sealed class BoardInitializer : IBoardInitializer
    {
        private readonly IShipPositioner _shipPositioner;
        private readonly ICellVerifier _cellVerifier;
        private readonly int _maxAttempts;


        public BoardInitializer(IShipPositioner shipPositioner, ICellVerifier cellVerifier, int maxAttempts)
        {
            _shipPositioner = shipPositioner;
            _cellVerifier = cellVerifier;
            _maxAttempts = Math.Max(1, maxAttempts);
            ;
        }


        public Board InitializedBoard(BoardSize boardSize, ISet<ShipConfiguration> shipConfigurations)
        {
            var board = new Board(boardSize, new ImmutableArray<Ship>());


            var shipsToCreate = shipConfigurations
                .SelectMany(config => Enumerable.Range(0, config.ShipsNumber).Select(_ => new {config.Name, config.ShipSize}));

            var ships = Enumerable.Empty<Ship>().ToImmutableArray();
            var positions = Enumerable.Empty<Cell>().ToImmutableArray();
            foreach (var shipToCreate in shipsToCreate)
            {
                var counter = 0;
                do
                {
                    var newShipPosition = _shipPositioner.ShipPositionsFor(board, shipToCreate.ShipSize).ToImmutableArray();
                    var updatedPositions = positions.AddRange(newShipPosition);

                    var doShipsIntersectEachOther = _cellVerifier.CellsIntersect(updatedPositions);
                    if (!doShipsIntersectEachOther)
                    {
                        ships = ships.Add(new Ship(shipToCreate.Name, newShipPosition));
                        positions = updatedPositions;
                        break;
                    }

                    counter++;
                } while (counter <= _maxAttempts);

                if (counter > _maxAttempts)
                {
                    throw new CannotInitializeBoardException();
                }
            }


            return new Board(boardSize, ships);
        }
    }
}
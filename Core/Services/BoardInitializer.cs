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
        private readonly ICellVerifier _cellVerifier;
        private readonly int _maxAttempts;
        private readonly IShipPositioner _shipPositioner;


        public BoardInitializer(IShipPositioner shipPositioner, ICellVerifier cellVerifier, int maxAttempts)
        {
            _shipPositioner = shipPositioner;
            _cellVerifier = cellVerifier;
            _maxAttempts = Math.Max(1, maxAttempts);
        }


        public Board InitializedBoard(BoardSize boardSize, ISet<ShipConfiguration> shipConfigurations)
        {
            var board = new Board(boardSize, new ImmutableArray<Ship>());


            var shipsToCreate = shipConfigurations
                .SelectMany(config => Enumerable.Range(0, config.ShipsNumber).Select(_ => new {config.Name, config.ShipSize}));

            var ships = Enumerable.Empty<Ship>().ToImmutableArray();

            foreach (var shipToCreate in shipsToCreate)
            {
                var counter = 0;
                do
                {
                    try
                    {
                        var newShip = CreateNewShip(board, shipToCreate.Name, shipToCreate.ShipSize, ships);

                        if (newShip is not null)
                        {
                            ships = ships.Add(newShip);
                            break;
                        }

                        counter++;
                    }
                    catch (CannotCreateShipPositionsException createShipPositionsException)
                    {
                        throw new CannotInitializeBoardException(createShipPositionsException.Message);
                    }
                } while (counter <= _maxAttempts);

                if (counter > _maxAttempts)
                {
                    throw new CannotInitializeBoardException();
                }
            }


            return new Board(boardSize, ships);
        }


        private Ship? CreateNewShip(Board board, string shipName, int shipSize, ImmutableArray<Ship> ships)
        {
            var newShipPosition = _shipPositioner.ShipPositionsFor(board, shipSize).ToImmutableArray();
            var updatedPositions = ships.SelectMany(ship => ship.Cells)
                                        .ToImmutableArray()
                                        .AddRange(newShipPosition);

            var doShipsIntersectEachOther = _cellVerifier.CellsIntersect(updatedPositions);
            var newShip = new Ship(shipName, newShipPosition);

            return doShipsIntersectEachOther ? null : newShip;
        }
    }
}
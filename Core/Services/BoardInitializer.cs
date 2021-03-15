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
                        var newShipPosition = _shipPositioner.ShipPositionsFor(board, shipToCreate.ShipSize).ToImmutableArray();
                        var updatedPositions = ships.SelectMany(ship => ship.Cells).ToImmutableArray().AddRange(newShipPosition);

                        var doShipsIntersectEachOther = _cellVerifier.CellsIntersect(updatedPositions);
                        var newShip = new Ship(shipToCreate.Name, newShipPosition);
                        
                        if (!doShipsIntersectEachOther)
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


        private static Ship? CreateFor(string name, int size, Board board, IEnumerable<Ship> allCurrentlyCreatedShips,
                                       IShipPositioner shipPositioner, ICellVerifier cellVerifier)
        {
            var newShipPosition = shipPositioner.ShipPositionsFor(board, size).ToImmutableArray();
            var updatedPositions = allCurrentlyCreatedShips.SelectMany(s => s.Cells).ToImmutableArray().AddRange(newShipPosition);

            var doShipsIntersectEachOther = cellVerifier.CellsIntersect(updatedPositions);
            return !doShipsIntersectEachOther
                ? new Ship(name, newShipPosition)
                : null;
        }
    }
}
using System.Collections.Generic;
using System.Collections.Immutable;
using Core.Model;
using Core.Services;
using CoreTests.Utils;
using FluentAssertions;
using Xunit;

namespace CoreTests.Services
{
    public class BoardVerifierTest
    {
        private readonly BoardVerifier _sut;


        public BoardVerifierTest()
        {
            _sut = new BoardVerifier();
        }


        [Theory(DisplayName = "BoardVerifier correctly detects when some Ships overlap")]
        [MemberData(nameof(OverlappingShipsTestData))]
        public void BoardVerifier_correctly_detects_when_some_Ships_overlap(IEnumerable<Ship> ships)
        {
            BoardSize boardSize = new(0, 0);
            var board = new Board(boardSize, ships.ToImmutableList());


            _sut.ShipsIntersectEachOther(board).Should()
                .BeTrue("provided Ships share some of the cells - they have the same coordinates");
        }


        public static IEnumerable<object[]> OverlappingShipsTestData()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new[]
                    {
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(0, 0).Build(),
                            new CellBuilder().WithCoordinates(0, 1).Build()
                        }).Build(),
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(0, 0).Build(),
                            new CellBuilder().WithCoordinates(1, 0).Build()
                        }).Build()
                    }
                },
                new object[]
                {
                    new[]
                    {
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(25, 2).Build(),
                            new CellBuilder().WithCoordinates(25, 3).Build(),
                            new CellBuilder().WithCoordinates(26, 3).Build()
                        }).Build(),
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(0, 0).Build(),
                            new CellBuilder().WithCoordinates(1, 0).Build()
                        }).Build(),
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(26, 5).Build(),
                            new CellBuilder().WithCoordinates(26, 4).Build(),
                            new CellBuilder().WithCoordinates(26, 3).Build()
                        }).Build()
                    }
                }
            };
        }


        [Theory(DisplayName = "BoardVerifier correctly detects when Ships don't overlap")]
        [MemberData(nameof(NonOverlappingShipsTestData))]
        public void BoardVerifier_correctly_detects_when_Ships_do_not_overlap(IEnumerable<Ship> ships)
        {
            BoardSize boardSize = new(0, 0);
            var board = new Board(boardSize, ships.ToImmutableList());


            _sut.ShipsIntersectEachOther(board).Should()
                .BeFalse("provided Ships don't the cells - they all have different coordinates");
        }


        public static IEnumerable<object[]> NonOverlappingShipsTestData()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new[]
                    {
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(0, 0).Build(),
                            new CellBuilder().WithCoordinates(0, 1).Build()
                        }).Build(),
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(2, 0).Build(),
                            new CellBuilder().WithCoordinates(2, 1).Build()
                        }).Build()
                    }
                },
                new object[]
                {
                    new[]
                    {
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(100, 1).Build(),
                            new CellBuilder().WithCoordinates(100, 2).Build(),
                            new CellBuilder().WithCoordinates(101, 2).Build()
                        }).Build(),
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(0, 0).Build(),
                            new CellBuilder().WithCoordinates(1, 0).Build()
                        }).Build(),
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(26, 5).Build(),
                            new CellBuilder().WithCoordinates(26, 4).Build(),
                            new CellBuilder().WithCoordinates(26, 3).Build()
                        }).Build()
                    }
                }
            };
        }
    }
}
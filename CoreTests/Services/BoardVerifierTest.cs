using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Core.Exceptions;
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


        [Theory(DisplayName = "BoardVerifier throws exception if any of the Ships has negative coordinates")]
        [MemberData(nameof(ShipsWithNegativeCoordinates))]
        public void BoardVerifier_throws_exception_if_any_of_the_Ships_has_negative_coordinates(IEnumerable<Ship> allShips,
            IEnumerable<Ship> invalidShips)
        {
            BoardSize boardSize = new(0, 0);
            var board = new Board(boardSize, allShips.ToImmutableList());


            Action act = () => _sut.ShipsAreWithinBounds(board);


            act.Should().Throw<ShipsNegativeCoordinatesException>()
               .WithMessage(string.Join(',', invalidShips.Select(ship => ship.ToString())),
                            $"{nameof(ShipsNegativeCoordinatesException)} should be thrown with a list of invalid Ships");
        }


        public static IEnumerable<object[]> ShipsWithNegativeCoordinates()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new[]
                    {
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(-100, 1).Build(),
                            new CellBuilder().WithCoordinates(25, 1).Build()
                        }).Build(),
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(-3, 4).Build()
                        }).Build()
                    },
                    new[]
                    {
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(-100, 1).Build(),
                            new CellBuilder().WithCoordinates(25, 1).Build()
                        }).Build(),
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(-3, 4).Build()
                        }).Build()
                    }
                },
                new object[]
                {
                    new[]
                    {
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(15, 15).Build(),
                            new CellBuilder().WithCoordinates(25, 1).Build()
                        }).Build(),
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(13, -6).Build()
                        }).Build()
                    },
                    new[]
                    {
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(13, -6).Build()
                        }).Build()
                    }
                },
                new object[]
                {
                    new[]
                    {
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(-100, 1).Build()
                        }).Build()
                    },
                    new[]
                    {
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(-100, 1).Build()
                        }).Build()
                    }
                },
                new object[]
                {
                    new[]
                    {
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(-100, -1).Build()
                        }).Build()
                    },
                    new[]
                    {
                        new ShipBuilder().WithCells(new[]
                        {
                            new CellBuilder().WithCoordinates(-100, -1).Build()
                        }).Build()
                    }
                }
            };
        }


        [Theory(DisplayName = "BoardVerifier correctly verifies if Ships are within bounds of a Board")]
        [MemberData(nameof(ShipsWithinBoardBoundsCoordinates))]
        public void BoardVerifier_correctly_throws_exception_if_any_of_the_Ships_has_negative_coordinates(Board board,
            bool expectedResult)
        {
            _sut.ShipsAreWithinBounds(board).Should().Be(expectedResult);
        }


        public static IEnumerable<object[]> ShipsWithinBoardBoundsCoordinates()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new Board(new BoardSize(10, 10),
                              new[]
                              {
                                  new ShipBuilder().WithCells(new[]
                                  {
                                      new CellBuilder().WithCoordinates(0, 0).Build()
                                  }).Build(),
                                  new ShipBuilder().WithCells(new[]
                                  {
                                      new CellBuilder().WithCoordinates(2, 1).Build()
                                  }).Build()
                              }.ToImmutableList()),
                    true
                },
                new object[]
                {
                    new Board(new BoardSize(100, 75),
                              new[]
                              {
                                  new ShipBuilder().WithCells(new[]
                                  {
                                      new CellBuilder().WithCoordinates(100, 75).Build()
                                  }).Build()
                              }.ToImmutableList()),
                    true
                },
                new object[]
                {
                    new Board(new BoardSize(75, 100),
                              new[]
                              {
                                  new ShipBuilder().WithCells(new[]
                                  {
                                      new CellBuilder().WithCoordinates(100, 75).Build()
                                  }).Build()
                              }.ToImmutableList()),
                    false
                },
                new object[]
                {
                    new Board(new BoardSize(10, 10),
                              new[]
                              {
                                  new ShipBuilder().WithCells(new[]
                                  {
                                      new CellBuilder().WithCoordinates(10, 10).Build()
                                  }).Build(),
                                  new ShipBuilder().WithCells(new[]
                                  {
                                      new CellBuilder().WithCoordinates(10, 10).Build()
                                  }).Build()
                              }.ToImmutableList()),
                    true
                },
                new object[]
                {
                    new Board(new BoardSize(10, 10),
                              new[]
                              {
                                  new ShipBuilder().WithCells(new[]
                                  {
                                      new CellBuilder().WithCoordinates(25, 25).Build()
                                  }).Build(),
                                  new ShipBuilder().WithCells(new[]
                                  {
                                      new CellBuilder().WithCoordinates(25, 25).Build()
                                  }).Build()
                              }.ToImmutableList()),
                    false
                },
                new object[]
                {
                    new Board(new BoardSize(25, 25),
                              new[]
                              {
                                  new ShipBuilder().WithCells(new[]
                                  {
                                      new CellBuilder().WithCoordinates(25, 25).Build()
                                  }).Build(),
                                  new ShipBuilder().WithCells(new[]
                                  {
                                      new CellBuilder().WithCoordinates(25, 25).Build()
                                  }).Build()
                              }.ToImmutableList()),
                    true
                },
                new object[]
                {
                    new Board(new BoardSize(10, 10),
                              new[]
                              {
                                  new ShipBuilder().WithCells(new[]
                                  {
                                      new CellBuilder().WithCoordinates(0, 0).Build()
                                  }).Build(),
                                  new ShipBuilder().WithCells(new[]
                                  {
                                      new CellBuilder().WithCoordinates(2, 11).Build()
                                  }).Build()
                              }.ToImmutableList()),
                    false
                },
                new object[]
                {
                    new Board(new BoardSize(10, 10),
                              new[]
                              {
                                  new ShipBuilder().WithCells(new[]
                                  {
                                      new CellBuilder().WithCoordinates(100, 0).Build()
                                  }).Build(),
                                  new ShipBuilder().WithCells(new[]
                                  {
                                      new CellBuilder().WithCoordinates(12, 8).Build()
                                  }).Build()
                              }.ToImmutableList()),
                    false
                }
            };
        }
    }
}
using System.Collections.Generic;
using Core.Model;
using Core.Services;
using CoreTests.Utils;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoreTests.Services
{
    public class BoardInitializerTest
    {
        private readonly BoardInitializer _sut;
        private readonly Mock<IShipPositioner> _shipPositioner = new();
        private readonly Mock<ICellVerifier> _cellVerifier = new();


        public BoardInitializerTest()
        {
            _sut = new BoardInitializer(_shipPositioner.Object, _cellVerifier.Object);
        }


        [Theory(DisplayName = "All ships are placed on a board")]
        [MemberData(nameof(PlacingShipsTestData))]
        public void All_ships_are_placed_on_board(ISet<ShipConfiguration> shipConfigurations, int expectedNumberOfPlacedShips)
        {
            BoardSize boardSize = new(0, 0);

            var initializedBoard = _sut.InitializedBoard(boardSize, shipConfigurations);

            initializedBoard.Ships.Should().HaveCount(expectedNumberOfPlacedShips,
                                                      "number of placed ships should equal a sum of ships from shipConfigurations");
        }


        [Theory(DisplayName = "Ships positions are determined by IShipPositioner")]
        [MemberData(nameof(PlacingShipsTestData))]
        public void Ships_positions_are_determined_by_IShipPositioner(ISet<ShipConfiguration> shipConfigurations,
                                                                      int expectedNumberOfPlacedShips)
        {
            BoardSize boardSize = new(0, 0);

            var initializedBoard = _sut.InitializedBoard(boardSize, shipConfigurations);

            initializedBoard.Ships.Should().HaveCount(expectedNumberOfPlacedShips,
                                                      "number of placed ships should equal a sum of ships from shipConfigurations");
        }


        public static IEnumerable<object[]> PlacingShipsTestData()
        {
            return new List<object[]>
            {
                new object[]
                {
                    new HashSet<ShipConfiguration>(new[]
                    {
                        new ShipConfigurationBuilder().WithShipSize(1).WithShipsNumber(2).Build(),
                        new ShipConfigurationBuilder().WithShipSize(2).WithShipsNumber(4).Build(),
                    }),
                    6
                },
                new object[]
                {
                    new HashSet<ShipConfiguration>(new[]
                    {
                        new ShipConfigurationBuilder().WithShipSize(4).WithShipsNumber(2).Build(),
                        new ShipConfigurationBuilder().WithShipSize(1).WithShipsNumber(2).Build(),
                    }),
                    4
                },
                new object[]
                {
                    new HashSet<ShipConfiguration>(new[]
                    {
                        new ShipConfigurationBuilder().WithShipSize(2).WithShipsNumber(5).Build()
                    }),
                    5
                }
            };
        }
    }
}
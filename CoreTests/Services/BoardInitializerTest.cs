using System.Collections.Generic;
using Core.Model;
using Core.Services;
using FluentAssertions;
using Xunit;

namespace CoreTests.Services
{
    public class BoardInitializerTest
    {
        private readonly BoardInitializer _sut;


        public BoardInitializerTest()
        {
            _sut = new BoardInitializer();
        }


        [Theory]
        [MemberData(nameof(PlacingShipsTestData))]
        public void All_ships_are_placed_on_board(ISet<ShipConfiguration> shipConfigurations, int expectedNumberOfPlacedShips)
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
                    new HashSet<ShipConfiguration>(new[] {new ShipConfiguration(1, 2), new ShipConfiguration(2, 4)}), 6
                },
                new object[]
                {
                    new HashSet<ShipConfiguration>(new[] {new ShipConfiguration(4, 2), new ShipConfiguration(1, 2)}), 4
                },
                new object[] {new HashSet<ShipConfiguration>(new[] {new ShipConfiguration(2, 5)}), 5}
            };
        }
    }
}
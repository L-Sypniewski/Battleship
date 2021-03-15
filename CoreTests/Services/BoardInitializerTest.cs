using System;
using System.Collections.Generic;
using Core.Model;
using Core.Services;
using CoreTests.TestData.Services.BoardVerifier.BoardInitializer;
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
            _sut = new BoardInitializer(_shipPositioner.Object, _cellVerifier.Object, 10);
        }


        private void SetupShipPositionerToReturn(IReadOnlyCollection<Cell> cells)
        {
            _shipPositioner.Setup(mock => mock.ShipPositionsFor(It.IsAny<Board>(), It.IsAny<int>())).Returns(cells);
        }


        private void SetupCellIntersectionVerifierToReturn(bool result)
        {
            _cellVerifier.Setup(mock => mock.CellsIntersect(It.IsAny<IEnumerable<Cell>>())).Returns(result);
        }


        [Theory(DisplayName = "All ships are placed on a board if all verifications succeeded")]
        [ClassData(typeof(NumberOfPlacedShipsClassData))]
        public void All_ships_are_placed_on_board_if_all_verifications_succeeded(
            ISet<ShipConfiguration> shipConfigurations, int expectedNumberOfPlacedShips)
        {
            SetupShipPositionerToReturn(Array.Empty<Cell>());
            SetupCellIntersectionVerifierToReturn(false);

            BoardSize boardSize = new(0, 0);
            var initializedBoard = _sut.InitializedBoard(boardSize, shipConfigurations);

            initializedBoard.Ships.Should().HaveCount(expectedNumberOfPlacedShips,
                                                      "number of placed ships should equal a sum of ships from shipConfigurations");
        }
    }
}
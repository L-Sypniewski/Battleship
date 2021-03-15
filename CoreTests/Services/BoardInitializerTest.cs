using System;
using System.Collections.Generic;
using Core.Model;
using Core.Services;
using CoreTests.TestData.Services.BoardVerifier.BoardInitializer;
using CoreTests.Utils;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoreTests.Services
{
    public class BoardInitializerTest
    {
        private BoardInitializer _sut;
        private readonly Mock<IShipPositioner> _shipPositioner = new();
        private readonly Mock<ICellVerifier> _cellVerifier = new();


        public BoardInitializerTest()
        {
            const int maxAttempts = 1;
            _sut = CreateSut(maxAttempts);
        }


        private BoardInitializer CreateSut(int maxAttempts) => new(_shipPositioner.Object, _cellVerifier.Object, maxAttempts);


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


        [Theory(DisplayName = "All ships are created with a correct name and position")]
        [ClassData(typeof(CreatingShipsClassData))]
        public void All_ships_are_created_with_correct_name_and_position(ISet<ShipConfiguration> shipConfigurations,
                                                                         Cell[] positionsCreatedByPositioner,
                                                                         IEnumerable<Ship> expectedCreatedShips)
        {
            SetupShipPositionerToReturn(positionsCreatedByPositioner);
            SetupCellIntersectionVerifierToReturn(false);
            BoardSize boardSize = new(0, 0);


            var initializedBoard = _sut.InitializedBoard(boardSize, shipConfigurations);


            initializedBoard.Ships.Should().BeEquivalentTo(expectedCreatedShips,
                                                           "created Ships should have values assigned from ShipPositioner and ShipConfiguration");
        }


        [Theory(DisplayName = "Services are called once for each Ship if all verifications succeeded")]
        [ClassData(typeof(NumberOfPlacedShipsClassData))]
        public void Services_are_called_once_for_each_Ship_if_all_verifications_succeeded(
            ISet<ShipConfiguration> shipConfigurations, int expectedNumberOfCalls)
        {
            SetupShipPositionerToReturn(Array.Empty<Cell>());
            SetupCellIntersectionVerifierToReturn(false);

            BoardSize boardSize = new(0, 0);


            var _ = _sut.InitializedBoard(boardSize, shipConfigurations);


            _shipPositioner.Verify(mock => mock.ShipPositionsFor(It.IsAny<Board>(), It.IsAny<int>()),
                                   Times.Exactly(expectedNumberOfCalls));
            _cellVerifier.Verify(mock => mock.CellsIntersect(It.IsAny<IEnumerable<Cell>>()),
                                 Times.Exactly(expectedNumberOfCalls));
        }


        [Fact(DisplayName = "Board is not returned unless all verifications pass")]
        public void Board_is_not_returned_unless_all_verifications_pass()
        {
            const int numberOfFailedVerifications = 4;
            const int maxAttempts = numberOfFailedVerifications + 1;
            _sut = CreateSut(maxAttempts);
            
            SetupShipPositionerToReturn(Array.Empty<Cell>());
            _cellVerifier.SetupSequence(mock => mock.CellsIntersect(It.IsAny<IEnumerable<Cell>>()))
                         .Returns(true)
                         .Returns(true)
                         .Returns(true)
                         .Returns(true)
                         .Returns(false);

            var shipConfiguration = new ShipConfigurationBuilder().WithShipsNumber(1).Build();
            BoardSize boardSize = new(0, 0);


            var _ = _sut.InitializedBoard(boardSize, new HashSet<ShipConfiguration>(new[] {shipConfiguration}));


            const int expectedNumberOfServiceCalls = numberOfFailedVerifications + 1;
            _shipPositioner.Verify(mock => mock.ShipPositionsFor(It.IsAny<Board>(), It.IsAny<int>()),
                                   Times.Exactly(expectedNumberOfServiceCalls));
            _cellVerifier.Verify(mock => mock.CellsIntersect(It.IsAny<IEnumerable<Cell>>()),
                                 Times.Exactly(expectedNumberOfServiceCalls));
        }
    }
}
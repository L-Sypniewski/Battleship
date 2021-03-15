using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Core.Exceptions;
using Core.Model;
using Core.Services;
using CoreTests.TestData.Services.ShipPositioner;
using CoreTests.Utils;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoreTests.Services
{
    [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
    public class ShipPositionerTest
    {
        private readonly Mock<IBoardVerifier> _boardVerifier = new();
        private readonly Mock<ICellRandomizer> _cellRandomizer = new();

        private readonly Mock<IShipOrientationRandomizer> _shipOrientationRandomizer = new();
        private ShipPositioner _sut;


        public ShipPositionerTest()
        {
            const int maxAttempts = 1;
            _sut = CreateSut(maxAttempts);
        }


        private ShipPositioner CreateSut(int maxAttemps) => new(
            maxAttemps, _shipOrientationRandomizer.Object, _cellRandomizer.Object, _boardVerifier.Object);


        [Theory(DisplayName = "Ship are correctly placed in horizontal position")]
        [ClassData(typeof(PositioningShipsHorizontallyClassData))]
        public void Ship_are_correctly_placed_in_horizontal_position(Cell firstCell, int shipSize, Cell[] expectedCells)
        {
            _cellRandomizer.Setup(mock => mock.GetCellWithin(It.IsAny<BoardSize>()))
                           .Returns(firstCell);
            _boardVerifier.Setup(mock => mock.CellsAreWithinBounds(It.IsAny<BoardSize>(), It.IsAny<IEnumerable<Cell>>()))
                          .Returns(true);
            _shipOrientationRandomizer.Setup(mock => mock.GetOrientation())
                                      .Returns(ShipOrientation.Horizontal);


            var shipCells = _sut.ShipPositionsFor(new BoardBuilder().Build(), shipSize);


            shipCells.Should().BeEquivalentTo(expectedCells);
        }


        [Theory(DisplayName = "Ship are correctly placed in vertical position")]
        [ClassData(typeof(PositioningShipsVerticallyClassData))]
        public void Ship_are_correctly_placed_in_vertical_position(Cell firstCell, int shipSize, Cell[] expectedCells)
        {
            _cellRandomizer.Setup(mock => mock.GetCellWithin(It.IsAny<BoardSize>()))
                           .Returns(firstCell);
            _boardVerifier.Setup(mock => mock.CellsAreWithinBounds(It.IsAny<BoardSize>(), It.IsAny<IEnumerable<Cell>>()))
                          .Returns(true);
            _shipOrientationRandomizer.Setup(mock => mock.GetOrientation())
                                      .Returns(ShipOrientation.Vertical);


            var shipCells = _sut.ShipPositionsFor(new BoardBuilder().Build(), shipSize);


            shipCells.Should().BeEquivalentTo(expectedCells);
        }


        [Theory(DisplayName = "New position is not returned unless it is verified to be within Board bounds")]
        [InlineData(ShipOrientation.Horizontal)]
        [InlineData(ShipOrientation.Vertical)]
        public void New_position_is_not_returned_unless_it_is_verified_to_be_within_Board_bounds(ShipOrientation shipOrientation)
        {
            const int numberOfFailedVerifications = 3;
            const int maxAttempts = numberOfFailedVerifications + 1;
            _sut = CreateSut(maxAttempts);

            var firstCell = new Cell(0, 0, false);
            _cellRandomizer.Setup(mock => mock.GetCellWithin(It.IsAny<BoardSize>()))
                           .Returns(firstCell);

            _shipOrientationRandomizer.Setup(mock => mock.GetOrientation())
                                      .Returns(shipOrientation);

            _boardVerifier.SetupSequence(mock => mock.CellsAreWithinBounds(It.IsAny<BoardSize>(), It.IsAny<IEnumerable<Cell>>()))
                          .Returns(false)
                          .Returns(false)
                          .Returns(false)
                          .Returns(true);


            var _ = _sut.ShipPositionsFor(new BoardBuilder().Build(), 1);


            const int expectedNumberOfServiceCalls = numberOfFailedVerifications + 1;
            _cellRandomizer.Verify(mock => mock.GetCellWithin(It.IsAny<BoardSize>()),
                                   Times.Exactly(expectedNumberOfServiceCalls));
            _shipOrientationRandomizer.Verify(mock => mock.GetOrientation(), Times.Exactly(expectedNumberOfServiceCalls));
        }


        [Theory(DisplayName = "Exception is thrown if could not create valid positions after defined number of max attempts")]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void Exception_is_thrown_if_could_not_create_valid_positions_after_defined_number_of_max_attempts(int maxAttempts)
        {
            _sut = CreateSut(maxAttempts);

            var firstCell = new Cell(0, 0, false);
            _cellRandomizer.Setup(mock => mock.GetCellWithin(It.IsAny<BoardSize>()))
                           .Returns(firstCell);

            _boardVerifier.Setup(mock => mock.CellsAreWithinBounds(It.IsAny<BoardSize>(), It.IsAny<IEnumerable<Cell>>()))
                          .Returns(false);

            
            Action act = () => _sut.ShipPositionsFor(new BoardBuilder().Build(), shipSize: 1);


            act.Should().Throw<CannotCreateShipPositionsException>();
        }
    }
}
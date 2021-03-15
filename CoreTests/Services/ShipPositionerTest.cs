using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Core.Model;
using Core.Services;
using CoreTests.TestData.Services.CellVerifier;
using CoreTests.TestData.Services.ShipPositioner;
using CoreTests.Utils;
using FluentAssertions;
using Moq;
using Xunit;

namespace CoreTests.Services
{
    public class ShipPositionerTest
    {
        private readonly ShipPositioner _sut;

        private readonly Mock<IShipOrientationRandomizer> _shipOrientationRandomizer = new();
        private readonly Mock<ICellRandomizer> _cellRandomizer = new();
        private readonly Mock<IBoardVerifier> _boardVerifier = new();


        public ShipPositionerTest()
        {
            _sut = new ShipPositioner(_shipOrientationRandomizer.Object, _cellRandomizer.Object, _boardVerifier.Object);
        }


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
               var firstCell = new Cell(0, 0, false);
               _cellRandomizer.Setup(mock => mock.GetCellWithin(It.IsAny<BoardSize>()))
                              .Returns(firstCell);
   
               _shipOrientationRandomizer.Setup(mock => mock.GetOrientation())
                                         .Returns(shipOrientation);
   
               const int numberOfFailedVerifications = 4;
               _boardVerifier.SetupSequence(mock => mock.CellsAreWithinBounds(It.IsAny<BoardSize>(),It.IsAny<IEnumerable<Cell>>()))
                             .Returns(false)
                             .Returns(false)
                             .Returns(false)
                             .Returns(true);
   
               var _ = _sut.ShipPositionsFor(new BoardBuilder().WithSize(new BoardSize(1, 1)).Build(), 1);
   
   
               _cellRandomizer.Verify(mock => mock.GetCellWithin(It.IsAny<BoardSize>()), Times.Exactly(numberOfFailedVerifications));
               _shipOrientationRandomizer.Verify(mock => mock.GetOrientation(), Times.Exactly(numberOfFailedVerifications));
           }
    }
}
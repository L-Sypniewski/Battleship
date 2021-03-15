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

        private readonly Mock<IShipOrientationRandomizer> _shipOrientationRandomizer = new Mock<IShipOrientationRandomizer>();
        private readonly Mock<ICellRandomizer> _cellRandomizer = new Mock<ICellRandomizer>();
        private readonly Mock<IBoardVerifier> _boardVerifier = new Mock<IBoardVerifier>();


        public ShipPositionerTest()
        {
            _sut = new ShipPositioner(_shipOrientationRandomizer.Object, _cellRandomizer.Object, _boardVerifier.Object);
        }


        [Theory(DisplayName = "Ship are correctly placed in horizontal position")]
        [ClassData(typeof(PositiningShipsHorizontallyClassData))]
        public void Ship_are_correctly_placed_in_horizontal_position(Cell firstCell, int shipSize, Cell[] expectedCells)
        {
            _cellRandomizer.Setup(mock => mock.GetCellWithin(It.IsAny<BoardSize>()))
                           .Returns(firstCell);
            _boardVerifier.Setup(mock => mock.ShipsAreWithinBounds(It.IsAny<Board>()))
                          .Returns(true);
            _shipOrientationRandomizer.Setup(mock => mock.GetOrientation())
                                      .Returns(ShipOrientation.Horizontal);

            var shipCells = _sut.ShipPositionsFor(new BoardBuilder().Build(), shipSize);
            
            
            shipCells.Should().BeEquivalentTo(expectedCells);
        }
    }
}
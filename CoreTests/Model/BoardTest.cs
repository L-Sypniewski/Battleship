using Core.Model;
using CoreTests.TestUtils;
using FluentAssertions;
using Xunit;

namespace CoreTests.Model
{
    public class BoardTest
    {
        [Theory(DisplayName = "Board returns correct state of a given Cell")]
        [InlineData(0, 0, CellState.Sunk)]
        [InlineData(1, 0, CellState.Sunk)]
        [InlineData(1, 1, CellState.Hit)]
        [InlineData(1, 2, CellState.Clear)]
        [InlineData(0, 1, CellState.Clear)]
        public void Board_returns_correct_state_of_a_given_Cell(int x, int y, CellState expectedCellState)
        {
            var board = CreateTestBoard();
            var cellToCheck = new CellBuilder().WithCoordinates(x, y).Build();
            board.CellState(cellToCheck).Should().Be(expectedCellState);
        }


        private static Board CreateTestBoard()
        {
            var boardSize = new BoardSize(2, 3);
            var sunkShip = new ShipBuilder()
                           .WithName("sunk")
                           .WithCells(new[]
                           {
                               new CellBuilder().WithCoordinates(0, 0).WithIsShot(true).Build(),
                               new CellBuilder().WithCoordinates(1, 0).WithIsShot(true).Build(),
                           }).Build();

            var hitShip = new ShipBuilder()
                          .WithName("hit")
                          .WithCells(new[]
                          {
                              new CellBuilder().WithCoordinates(1, 1).WithIsShot(true).Build(),
                              new CellBuilder().WithCoordinates(1, 2).Build(),
                          }).Build();

            return new BoardBuilder().WithSize(boardSize)
                                     .WithShips(new[] {sunkShip, hitShip})
                                     .Build();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Exceptions;
using Core.Model;
using Core.Services;
using CoreTests.TestData.Services.BoardVerifier;
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


        [Theory(DisplayName = "BoardVerifier throws exception if any of the Cells has negative coordinates")]
        [ClassData(typeof(ShipsWithNegativeCoordinatesClassData))]
        public void BoardVerifier_throws_exception_if_any_of_the_Cells_has_negative_coordinates(IEnumerable<Cell> allCells,
            IEnumerable<Cell> invalidCells)
        {
            BoardSize boardSize = new(0, 0);

            Action act = () => _sut.CellsAreWithinBounds(boardSize, allCells);


            act.Should().Throw<CellsNegativeCoordinatesException>()
               .WithMessage(string.Join(',', invalidCells.Select(ship => ship.ToString())),
                            $"{nameof(CellsNegativeCoordinatesException)} should be thrown with a list of invalid Cells");
        }


        [Theory(DisplayName = "BoardVerifier correctly verifies if Cells are within bounds of a BoardSize")]
        [ClassData(typeof(ShipsWithinBoardBoundsCoordinatesClassData))]
        public void BoardVerifier_correctly_verifies_if_Cells_are_within_bounds_of_a_BoardSize(BoardSize boardSize,
            IEnumerable<Cell> cells, bool expectedResult)
        {
            _sut.CellsAreWithinBounds(boardSize, cells).Should().Be(expectedResult);
        }
    }
}
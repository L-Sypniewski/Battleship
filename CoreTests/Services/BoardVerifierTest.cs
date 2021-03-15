using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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


        [Theory(DisplayName = "BoardVerifier correctly detects when some Ships overlap")]
        [ClassData(typeof(OverlappingShipsClassData))]
        public void BoardVerifier_correctly_detects_when_some_Ships_overlap(IEnumerable<Ship> ships)
        {
            BoardSize boardSize = new(0, 0);
            var board = new Board(boardSize, ships.ToImmutableList());


            _sut.ShipsIntersectEachOther(board).Should()
                .BeTrue("provided Ships share some of the cells - they have the same coordinates");
        }


        [Theory(DisplayName = "BoardVerifier correctly detects when Ships don't overlap")]
        [ClassData(typeof(NonOverlappingShipsClassData))]
        public void BoardVerifier_correctly_detects_when_Ships_do_not_overlap(IEnumerable<Ship> ships)
        {
            BoardSize boardSize = new(0, 0);
            var board = new Board(boardSize, ships.ToImmutableList());


            _sut.ShipsIntersectEachOther(board).Should()
                .BeFalse("provided Ships don't the cells - they all have different coordinates");
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
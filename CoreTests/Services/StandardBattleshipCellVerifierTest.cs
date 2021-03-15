using System.Collections.Generic;
using Core.Model;
using Core.Services;
using CoreTests.TestData.Services.CellVerifier;
using FluentAssertions;
using Xunit;

namespace CoreTests.Services
{
    public class StandardBattleshipCellVerifierTest
    {
        private readonly StandardBattleshipGameRulesCellVerifier _sut;


        public StandardBattleshipCellVerifierTest()
        {
            _sut = new StandardBattleshipGameRulesCellVerifier();
        }


        [Theory(DisplayName = "Verifier returns true if cells are adjacent and on the same axis")]
        [ClassData(typeof(ValidCellsClassData))]
        public void Verifier_returns_true_if_cells_are_adjacent_and_on_the_same_axis(IEnumerable<Cell> cells)
        {
            var isValid = _sut.Verify(cells);

            isValid.Should().BeTrue("All of provided Cell collections contain Cells that are adjacent and on the same axis");
        }


        [Theory(DisplayName = "Verifier returns false if cells are not adjacent or not on the same axis")]
        [ClassData(typeof(InvalidCellsClassData))]
        public void Verifier_returns_false_if_cells_are_not_adjacent_or_not_on_the_same_axis(IEnumerable<Cell> cells)
        {
            var isValid = _sut.Verify(cells);

            isValid.Should()
                   .BeFalse("All of provided Cell collections contain Cells that are not adjacent or not on the same axis");
        }
    }
}
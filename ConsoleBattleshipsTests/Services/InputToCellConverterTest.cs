using ConsoleBattleships.Services;
using Core.Model;
using FluentAssertions;
using Xunit;

namespace ConsoleBattleshipsTests.Services
{
    public class InputToCellConverterTest
    {
        private readonly InputToCellConverter _sut;


        public InputToCellConverterTest()
        {
            _sut = new InputToCellConverter();
        }


        [Theory(DisplayName = "Correct Cell is created for valid string input")]
        [InlineData("A1", 0, 0)]
        [InlineData("a1", 0, 0)]
        [InlineData("A2", 0, 1)]
        [InlineData("B1", 1, 0)]
        [InlineData("D13", 3, 12)]
        [InlineData("d13", 3, 12)]
        [InlineData("E0", 4, -1)]
        [InlineData("XG320", 630, 319)]
        [InlineData("xg320", 630, 319)]
        [InlineData("xG320", 630, 319)]
        [InlineData("Xg320", 630, 319)]
        public void Correct_Cell_is_created_for_a_valid_string_input(string input, int expectedCellXCoordinate,
                                                                     int expectedCellYCoordinate)
        {
            var expectedCell = new Cell(expectedCellXCoordinate, expectedCellYCoordinate, false);

            var actualCell = _sut.ConvertedFrom(input);

            actualCell.Should().Be(expectedCell);
        }


        [Theory(DisplayName = "Null Cell is returned for ivalid string input")]
        [InlineData("1A")]
        [InlineData("1a")]
        [InlineData("A-1")]
        [InlineData("A(-1)")]
        [InlineData("A")]
        [InlineData("a")]
        [InlineData("b")]
        [InlineData("_A1")]
        [InlineData(" A1")]
        [InlineData("A1 ")]
        [InlineData("A 1")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("!@#$%^&*(")]
        [InlineData("1")]
        [InlineData("foo bar")]
        public void Null_Cell_is_returned_for_invalid_string_input(string input)
        {
            var actualCell = _sut.ConvertedFrom(input);

            actualCell.Should().BeNull();
        }
    }
}
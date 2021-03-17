using Core.Utils;
using FluentAssertions;
using Xunit;

namespace CoreTests.Utils
{
    public class GetColumnNameTest
    {
        [Theory(DisplayName = "Number is correctly converted to column name as in Excel")]
        [InlineData(1, "A")]
        [InlineData(2, "B")]
        [InlineData(3, "C")]
        [InlineData(4, "D")]
        [InlineData(5, "E")]
        [InlineData(6, "F")]
        [InlineData(7, "G")]
        [InlineData(8, "H")]
        [InlineData(20, "T")]
        [InlineData(22, "V")]
        [InlineData(33, "AG")]
        public void Number_is_correctly_converted_to_column_name_as_in_Excel(int number, string expectedName)
        {
            number.ToColumnName().Should().Be(expectedName);
        }


        [Theory(DisplayName = "Column name is correctly converted to column index as in Excel")]
        [InlineData("A", 1)]
        [InlineData("B", 2)]
        [InlineData("C", 3)]
        [InlineData("D", 4)]
        [InlineData("E", 5)]
        [InlineData("F", 6)]
        [InlineData("G", 7)]
        [InlineData("H", 8)]
        [InlineData("T", 20)]
        [InlineData("V", 22)]
        [InlineData("AG", 33)]
        public void Column_name_is_correctly_converted_to_column_index_as_in_Excel(string columnName, int expectedIndex)
        {
            columnName.ToColumnIndex().Should().Be(expectedIndex);
        }
    }
}
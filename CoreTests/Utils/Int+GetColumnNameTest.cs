using Core.Utils;
using FluentAssertions;
using Xunit;

namespace CoreTests.Utils
{
    public class GetColumnNameTest
    {
        [Theory(DisplayName = "Number is correctly converted to column name as in Excel")]
        [InlineData(1,"A")]
        [InlineData(2,"B")]
        [InlineData(3,"C")]
        [InlineData(4,"D")]
        [InlineData(5,"E")]
        [InlineData(6,"F")]
        [InlineData(7,"G")]
        [InlineData(8,"H")]
        [InlineData(20,"T")]
        [InlineData(22,"V")]
        [InlineData(33,"AG")]
        public void Number_is_correctly_converted_to_column_name_as_in_Excel(int number, string expectedName)
        {
            number.ToColumnName().Should().Be(expectedName);
        }
    }
}
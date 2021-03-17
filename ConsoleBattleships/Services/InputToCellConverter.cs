using System.Text.RegularExpressions;
using ConsoleBattleships.Services.Interfaces;
using Core.Model;
using Core.Utils;

namespace ConsoleBattleships.Services
{
    public sealed class InputToCellConverter : IInputToCellConverter
    {
        public Cell? ConvertedFrom(string input)
        {
            const string regex = "^[Aa-zA-Z]{1}\\d{1,}$";
            var inputIsSingleLetterFollowedByNumber =
                Regex.IsMatch(input, regex, RegexOptions.Multiline);

            if (!inputIsSingleLetterFollowedByNumber)
            {
                return null;
            }

            var columnIndex = input[0].ToString().ToColumnIndex();
            var rowIndex = input[1];

            return new Cell(columnIndex, rowIndex, false);
        }
    }
}
using System.Linq;
using System.Text.RegularExpressions;
using ConsoleBattleships.Services.Interfaces;
using Core.Model;
using Core.Utils;

namespace ConsoleBattleships.Services
{
    public sealed class InputToCellConverter : IInputToCellConverter
    {
        public Cell? ConvertedFrom(string? input)
        {
            if (input is null)
            {
                return null;
            }

            const string regex = "^[Aa-zA-Z]{1,}\\d{1,}$";
            var inputIsLettersFollowedByNumber = Regex.IsMatch(input, regex, RegexOptions.Multiline);

            if (!inputIsLettersFollowedByNumber)
            {
                return null;
            }

            var firstNumber =  input.SkipWhile(c => !char.IsDigit(c))
                                                             .TakeWhile(char.IsDigit)
                                                             .Take(1)
                                                             .Single();
            var firstNumberIndex = input.IndexOf(firstNumber);
            
            var columnIndex = input[..firstNumberIndex].ToColumnIndex() - 1;
            var rowIndex = int.Parse(input[firstNumberIndex..]) - 1;

            return new Cell(columnIndex, rowIndex, false);
        }
    }
}
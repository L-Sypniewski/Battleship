using Core.Model;

namespace ConsoleBattleships.Services.Interfaces
{
    internal interface IInputToCellConverter
    {
        Cell? ConvertedFrom(string input);
    }
}
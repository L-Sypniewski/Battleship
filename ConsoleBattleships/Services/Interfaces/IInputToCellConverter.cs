using Core.Model;

namespace ConsoleBattleships.Services.Interfaces
{
    public interface IInputToCellConverter
    {
        Cell? ConvertedFrom(string input);
    }
}
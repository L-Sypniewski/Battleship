using Core.Model;

namespace Core.Services
{
    public interface ICellRandomizer
    {
        Cell GetCellWithin(BoardSize boardSize);
    }
}
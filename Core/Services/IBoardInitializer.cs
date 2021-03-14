using System.Collections.Generic;
using Core.Model;

namespace Core.Services
{
    public interface IBoardInitializer
    {
        Board InitializedBoard(Board board, IEnumerable<Ship> ships);
    }
}
using System.Collections.Generic;
using Core.Model;

namespace Core.Services
{
    public interface IBoardVerifier
    {
        /// <summary>
        ///     Checks if all <see cref="Cell" />s are withing bound of a given <see cref="BoardSize" />
        /// </summary>
        /// <param name="boardSize"></param>
        /// <param name="cells"></param>
        /// <returns></returns>
        bool CellsAreWithinBounds(BoardSize boardSize, IEnumerable<Cell> cells);
    }
}
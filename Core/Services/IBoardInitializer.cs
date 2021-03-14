using System.Collections.Generic;
using Core.Model;

namespace Core.Services
{
    public interface IBoardInitializer
    {
        /// <summary>
        ///     Returns initialized <see cref="Board" /> with a given ships configuration
        /// </summary>
        /// <param name="boardSize">Board size</param>
        /// <param name="shipConfigurations">Set containing ships configurations</param>
        /// <returns></returns>
        Board InitializedBoard(BoardSize boardSize, ISet<ShipConfiguration> shipConfigurations);
    }
}
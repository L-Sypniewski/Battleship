using System.Collections.Generic;
using Core.Model;

namespace Core.Services
{
    public interface ICellVerifier
    {
        /// <summary>
        ///     Allows to check if none of the <see cref="Cell" />s intersect
        /// </summary>
        /// <param name="cells">Cells to be verified</param>
        /// <returns></returns>
        public bool CellsIntersect(IEnumerable<Cell> cells);
    }
}
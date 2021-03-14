using Core.Model;

namespace Core.Services
{
    public interface IBoardVerifier
    {
        /// <summary>
        ///     Allows to check if all <see cref="Ship" />s occupy separate cells
        /// </summary>
        /// <param name="board">Board containing <see cref="Ship" />s to be verified</param>
        /// <returns></returns>
        public bool ShipsIntersectEachOther(Board board);
    }
}
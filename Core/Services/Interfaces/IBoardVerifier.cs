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


        /// <summary>
        ///     Checks if all <see cref="Ship" />s are withing bound of a given <see cref="Board" />
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        bool ShipsAreWithinBounds(Board board);
    }
}
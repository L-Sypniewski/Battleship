using System;

namespace Core.Exceptions
{
    public class CellsNegativeCoordinatesException : Exception
    {
        public CellsNegativeCoordinatesException()
        {
        }


        public CellsNegativeCoordinatesException(string message)
            : base(message)
        {
        }


        public CellsNegativeCoordinatesException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
using System;

namespace Core.Exceptions
{
    public class ShipsNegativeCoordinatesException : Exception
    {
        public ShipsNegativeCoordinatesException()
        {
        }


        public ShipsNegativeCoordinatesException(string message)
            : base(message)
        {
        }


        public ShipsNegativeCoordinatesException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
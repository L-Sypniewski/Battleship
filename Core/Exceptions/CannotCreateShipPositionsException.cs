using System;

namespace Core.Exceptions
{
    public class CannotCreateShipPositionsException : Exception
    {
        public CannotCreateShipPositionsException()
        {
        }


        public CannotCreateShipPositionsException(string message)
            : base(message)
        {
        }


        public CannotCreateShipPositionsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
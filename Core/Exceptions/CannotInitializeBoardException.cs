using System;

namespace Core.Exceptions
{
    public class CannotInitializeBoardException : Exception
    {
        public CannotInitializeBoardException()
        {
        }


        public CannotInitializeBoardException(string message)
            : base(message)
        {
        }


        public CannotInitializeBoardException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
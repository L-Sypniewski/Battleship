using System;

namespace Core.Exceptions
{
    public class CannotMakeOutOfBoundsShotException : Exception
    {
        public CannotMakeOutOfBoundsShotException()
        {
        }


        public CannotMakeOutOfBoundsShotException(string message)
            : base(message)
        {
        }


        public CannotMakeOutOfBoundsShotException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
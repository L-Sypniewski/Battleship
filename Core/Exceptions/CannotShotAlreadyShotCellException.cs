using System;

namespace Core.Exceptions
{
    public class CannotShotAlreadyShotCellException : Exception
    {
        public CannotShotAlreadyShotCellException()
        {
        }


        public CannotShotAlreadyShotCellException(string message)
            : base(message)
        {
        }


        public CannotShotAlreadyShotCellException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
using System;

namespace Core.Exceptions
{
    public class CannotCheckOutOfBoundsCellState : Exception
    {
        public CannotCheckOutOfBoundsCellState()
        {
        }


        public CannotCheckOutOfBoundsCellState(string message)
            : base(message)
        {
        }


        public CannotCheckOutOfBoundsCellState(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
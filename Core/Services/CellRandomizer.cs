using System;
using Core.Model;

namespace Core.Services
{
    public sealed class CellRandomizer : ICellRandomizer
    {
        private readonly Random _random = new();


        public Cell GetCellWithin(BoardSize boardSize)
        {
            var xCoordinate = _random.Next(0, boardSize.XSize + 1);
            var yCoordinate = _random.Next(0, boardSize.YSize + 1);
            return new Cell(xCoordinate, yCoordinate, false);
        }
    }
}
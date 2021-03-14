using System;
using Core.Model;

namespace CoreTests.Utils
{
    public class ShipBuilder
    {
        private string _name = "";
        private Cell[] _cells = Array.Empty<Cell>();


        public ShipBuilder WithName(string name)
        {
            _name = name;
            return this;
        }


        public ShipBuilder WithCells(Cell[] cells)
        {
            _cells = cells;
            return this;
        }


        public Ship Build()
        {
            return new Ship(_name, _cells);
        }
    }
}
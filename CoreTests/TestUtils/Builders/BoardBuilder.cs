using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Core.Model;

namespace CoreTests.TestUtils
{
    public class BoardBuilder
    {
        private BoardSize _boardSize = new(1, 1);
        private ImmutableArray<Cell> _cellsWithoutShips = Enumerable.Empty<Cell>().ToImmutableArray();
        private ImmutableArray<Ship> _ships = Enumerable.Empty<Ship>().ToImmutableArray();


        public BoardBuilder WithSize(BoardSize size)
        {
            _boardSize = size;
            return this;
        }


        public BoardBuilder WithShips(IEnumerable<Ship> ships)
        {
            _ships = ships.ToImmutableArray();
            return this;
        }


        public BoardBuilder WithCellsWithoutShips(IEnumerable<Cell> cellsWithoutShips)
        {
            _cellsWithoutShips = cellsWithoutShips.ToImmutableArray();
            return this;
        }


        public Board Build()
        {
            return new(_boardSize, _ships, _cellsWithoutShips);
        }
    }
}
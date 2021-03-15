using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Core.Model;

namespace CoreTests.Utils
{
    public class BoardBuilder
    {
        private BoardSize _boardSize = new(0, 0);
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


        public Board Build()
        {
            return new(_boardSize, _ships);
        }
    }
}
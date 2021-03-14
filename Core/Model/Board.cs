using System.Collections.Generic;

namespace Core.Model
{
    public sealed record Board(BoardSize Size, IReadOnlyCollection<Ship> Ships);
}
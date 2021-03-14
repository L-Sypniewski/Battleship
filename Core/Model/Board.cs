using System.Collections.Immutable;

namespace Core.Model
{
    public sealed record Board(BoardSize Size, IImmutableList<Ship> Ships);
}
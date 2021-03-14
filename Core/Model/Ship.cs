using System;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Core.Model
{
    public sealed record Ship(string Name, IImmutableList<Cell> Cells)
    {
        private bool PrintMembers(StringBuilder builder)
        {
            builder.Append($"{nameof(Name)}: {Name},");
            builder.Append($"{nameof(Cells)}: {string.Join(',', Cells.Select(cell => cell.ToString()))}");
            return true;
        }


        public bool Equals(Ship? other) => Name.Equals(other?.Name) ||
                                           Cells.SequenceEqual(other?.Cells ?? new ImmutableArray<Cell>());


        public override int GetHashCode() => HashCode.Combine(Name, Cells);
    }
}
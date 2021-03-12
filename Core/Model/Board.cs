using System.Collections.Generic;

namespace Core.Model
{
    public sealed record Board((int height, int width) Size, IReadOnlyCollection<Ship> Ships);
}
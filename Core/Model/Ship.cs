using System.Collections.Generic;
using System.Drawing;

namespace Core.Model
{
    public sealed record Ship(string Name, IDictionary<Point, bool> Cells);
}
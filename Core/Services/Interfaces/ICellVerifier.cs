using System.Collections.Generic;
using Core.Model;

namespace Core.Services
{
    public interface ICellVerifier
    {
        bool Verify(IEnumerable<Cell> cells);
    }
}
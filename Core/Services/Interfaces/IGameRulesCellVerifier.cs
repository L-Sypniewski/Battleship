using System.Collections.Generic;
using Core.Model;

namespace Core.Services
{
    public interface IGameRulesCellVerifier
    {
        bool Verify(IEnumerable<Cell> cells);
    }
}
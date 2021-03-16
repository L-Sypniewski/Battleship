using System.Collections;
using System.Collections.Generic;
using Core.Model;
using CoreTests.TestUtils;

namespace CoreTests.TestData.Services.BoardVerifier.BoardInitializer
{
    public class CreatingCellsWithoutShipsClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new BoardSize(3, 3),
                new[]
                {
                    new[]
                    {
                        new CellBuilder().WithCoordinates(0, 0).Build(),
                        new CellBuilder().WithCoordinates(0, 1).Build()
                    },
                    new[]
                    {
                        new CellBuilder().WithCoordinates(1,2).Build(),
                        new CellBuilder().WithCoordinates(2,2).Build()
                    }
                },
                new[] {
                    new CellBuilder().WithCoordinates(1,0).Build(),
                    new CellBuilder().WithCoordinates(2,0).Build(),
                    new CellBuilder().WithCoordinates(1,1).Build(),
                    new CellBuilder().WithCoordinates(2,1).Build(),
                    new CellBuilder().WithCoordinates(0,2).Build()
                }
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
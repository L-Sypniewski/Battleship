using System.Collections;
using System.Collections.Generic;
using CoreTests.TestUtils;

namespace CoreTests.TestData.Services.GameRulesCellVerifier
{
    public class InvalidCellsClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(0, 0).Build(),
                    new CellBuilder().WithCoordinates(1, 1).Build(),
                    new CellBuilder().WithCoordinates(0, 2).Build()
                }
            };
            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(0, 0).Build(),
                    new CellBuilder().WithCoordinates(0, 2).Build(),
                    new CellBuilder().WithCoordinates(0, 3).Build()
                }
            };
            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(1, 0).Build(),
                    new CellBuilder().WithCoordinates(3, 0).Build(),
                    new CellBuilder().WithCoordinates(4, 0).Build()
                }
            };
            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(1, 0).Build(),
                    new CellBuilder().WithCoordinates(2, 1).Build(),
                    new CellBuilder().WithCoordinates(3, 2).Build()
                }
            };
            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(24, 5).Build(),
                    new CellBuilder().WithCoordinates(25, 6).Build(),
                    new CellBuilder().WithCoordinates(24, 7).Build()
                }
            };
            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(0, 0).Build(),
                    new CellBuilder().WithCoordinates(0, 2).Build()
                }
            };
            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(2, 120).Build(),
                    new CellBuilder().WithCoordinates(2, 121).Build(),
                    new CellBuilder().WithCoordinates(2, 122).Build(),
                    new CellBuilder().WithCoordinates(2, 124).Build()
                }
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
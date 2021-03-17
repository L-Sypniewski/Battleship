using System.Collections;
using System.Collections.Generic;
using CoreTests.TestUtils;

namespace CoreTests.TestData.Services.CellVerifier
{
    public class IntersectingCellsClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(0, 1).Build(),
                    new CellBuilder().WithCoordinates(0, 1).Build()
                }
            };

            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(0, 1).WithIsShot(true).Build(),
                    new CellBuilder().WithCoordinates(0, 1).Build()
                }
            };

            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(1, 0).Build(),
                    new CellBuilder().WithCoordinates(1, 0).Build()
                }
            };

            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(25, 2).Build(),
                    new CellBuilder().WithCoordinates(26, 3).Build(),
                    new CellBuilder().WithCoordinates(26, 3).Build()
                }
            };

            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(26, 3).Build(),
                    new CellBuilder().WithCoordinates(26, 4).Build(),
                    new CellBuilder().WithCoordinates(26, 3).Build()
                }
            };

            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(26, 3).Build(),
                    new CellBuilder().WithCoordinates(26, 4).Build(),
                    new CellBuilder().WithCoordinates(26, 3).WithIsShot(true).Build()
                }
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
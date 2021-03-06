using System.Collections;
using System.Collections.Generic;
using CoreTests.TestUtils;

namespace CoreTests.TestData.Services.BoardVerifier
{
    public class ShipsWithNegativeCoordinatesClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(-100, 1).Build(),
                    new CellBuilder().WithCoordinates(25, 1).Build(),
                    new CellBuilder().WithCoordinates(-3, 4).Build()
                },
                new[]
                {
                    new CellBuilder().WithCoordinates(-100, 1).Build(),
                    new CellBuilder().WithCoordinates(-3, 4).Build()
                }
            };
            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(15, 15).Build(),
                    new CellBuilder().WithCoordinates(25, 1).Build(),
                    new CellBuilder().WithCoordinates(13, -6).Build()
                },

                new[]
                {
                    new CellBuilder().WithCoordinates(13, -6).Build()
                }
            };
            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(-100, 1).Build()
                },

                new[]
                {
                    new CellBuilder().WithCoordinates(-100, 1).Build()
                }
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
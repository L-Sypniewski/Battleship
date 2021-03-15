using System.Collections;
using System.Collections.Generic;
using CoreTests.Utils;

namespace CoreTests.TestData.Services.ShipPositioner
{
    public class PositioningShipsVerticallyClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new CellBuilder().WithCoordinates(0, 0).Build(),
                1,
                new[]
                {
                    new CellBuilder().WithCoordinates(0, 0).Build()
                }
            };

            yield return new object[]
            {
                new CellBuilder().WithCoordinates(0, 0).Build(),
                2,
                new[]
                {
                    new CellBuilder().WithCoordinates(0, 0).Build(),
                    new CellBuilder().WithCoordinates(0, 1).Build()
                }
            };
            yield return new object[]
            {
                new CellBuilder().WithCoordinates(4, 6).Build(),
                3,
                new[]
                {
                    new CellBuilder().WithCoordinates(4, 6).Build(),
                    new CellBuilder().WithCoordinates(4, 7).Build(),
                    new CellBuilder().WithCoordinates(4, 8).Build()
                }
            };
            yield return new object[]
            {
                new CellBuilder().WithCoordinates(-10, -5).Build(),
                4,
                new[]
                {
                    new CellBuilder().WithCoordinates(-10, -5).Build(),
                    new CellBuilder().WithCoordinates(-10, -4).Build(),
                    new CellBuilder().WithCoordinates(-10, -3).Build(),
                    new CellBuilder().WithCoordinates(-10, -2).Build()
                }
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
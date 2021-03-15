using System.Collections;
using System.Collections.Generic;
using CoreTests.Utils;

namespace CoreTests.TestData.Services.ShipPositioner
{
    public class PositioningShipsHorizontallyClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new CellBuilder().WithCoordinates(0, 0).Build(),
                1,
                new[]
                {
                    new CellBuilder().WithCoordinates(0, 0).Build(),
                }
            };

            yield return new object[]
            {
                new CellBuilder().WithCoordinates(0, 0).Build(),
                2,
                new[]
                {
                    new CellBuilder().WithCoordinates(0, 0).Build(),
                    new CellBuilder().WithCoordinates(1, 0).Build(),
                }
            };
            yield return new object[]
            {
                new CellBuilder().WithCoordinates(4, 6).Build(),
                3,
                new[]
                {
                    new CellBuilder().WithCoordinates(4, 6).Build(),
                    new CellBuilder().WithCoordinates(5, 6).Build(),
                    new CellBuilder().WithCoordinates(6, 6).Build(),
                }
            };
            yield return new object[]
            {
                new CellBuilder().WithCoordinates(-10, -5).Build(),
                4,
                new[]
                {
                    new CellBuilder().WithCoordinates(-10, -5).Build(),
                    new CellBuilder().WithCoordinates(-9, -5).Build(),
                    new CellBuilder().WithCoordinates(-8, -5).Build(),
                    new CellBuilder().WithCoordinates(-7, -5).Build(),
                }
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
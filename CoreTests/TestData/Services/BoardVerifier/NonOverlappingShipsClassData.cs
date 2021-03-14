using System.Collections;
using System.Collections.Generic;
using CoreTests.Utils;

namespace CoreTests.TestData.Services.BoardVerifier
{
    public class NonOverlappingShipsClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new[]
                {
                    new ShipBuilder().WithCells(new[]
                    {
                        new CellBuilder().WithCoordinates(0, 0).Build(),
                        new CellBuilder().WithCoordinates(0, 1).Build()
                    }).Build(),
                    new ShipBuilder().WithCells(new[]
                    {
                        new CellBuilder().WithCoordinates(2, 0).Build(),
                        new CellBuilder().WithCoordinates(2, 1).Build()
                    }).Build()
                }
            };

            yield return new object[]
            {
                new[]
                {
                    new ShipBuilder().WithCells(new[]
                    {
                        new CellBuilder().WithCoordinates(100, 1).Build(),
                        new CellBuilder().WithCoordinates(100, 2).Build(),
                        new CellBuilder().WithCoordinates(101, 2).Build()
                    }).Build(),
                    new ShipBuilder().WithCells(new[]
                    {
                        new CellBuilder().WithCoordinates(0, 0).Build(),
                        new CellBuilder().WithCoordinates(1, 0).Build()
                    }).Build(),
                    new ShipBuilder().WithCells(new[]
                    {
                        new CellBuilder().WithCoordinates(26, 5).Build(),
                        new CellBuilder().WithCoordinates(26, 4).Build(),
                        new CellBuilder().WithCoordinates(26, 3).Build()
                    }).Build()
                }
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
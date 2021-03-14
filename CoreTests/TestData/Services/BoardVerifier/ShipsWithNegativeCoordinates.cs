using System.Collections;
using System.Collections.Generic;
using CoreTests.Utils;

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
                    new ShipBuilder().WithCells(new[]
                    {
                        new CellBuilder().WithCoordinates(-100, 1).Build(),
                        new CellBuilder().WithCoordinates(25, 1).Build()
                    }).Build(),
                    new ShipBuilder().WithCells(new[]
                    {
                        new CellBuilder().WithCoordinates(-3, 4).Build()
                    }).Build()
                },
                new[]
                {
                    new ShipBuilder().WithCells(new[]
                    {
                        new CellBuilder().WithCoordinates(-100, 1).Build(),
                        new CellBuilder().WithCoordinates(25, 1).Build()
                    }).Build(),
                    new ShipBuilder().WithCells(new[]
                    {
                        new CellBuilder().WithCoordinates(-3, 4).Build()
                    }).Build()
                }
            };
            yield return new object[]
            {
                new[]
                {
                    new ShipBuilder().WithCells(new[]
                    {
                        new CellBuilder().WithCoordinates(15, 15).Build(),
                        new CellBuilder().WithCoordinates(25, 1).Build()
                    }).Build(),
                    new ShipBuilder().WithCells(new[]
                    {
                        new CellBuilder().WithCoordinates(13, -6).Build()
                    }).Build()
                },
                new[]
                {
                    new ShipBuilder().WithCells(new[]
                    {
                        new CellBuilder().WithCoordinates(13, -6).Build()
                    }).Build()
                }
            };
            yield return new object[]
            {
                new[]
                {
                    new ShipBuilder().WithCells(new[]
                    {
                        new CellBuilder().WithCoordinates(-100, 1).Build()
                    }).Build()
                },
                new[]
                {
                    new ShipBuilder().WithCells(new[]
                    {
                        new CellBuilder().WithCoordinates(-100, 1).Build()
                    }).Build()
                }
            };
            yield return new object[]
            {
                new[]
                {
                    new ShipBuilder().WithCells(new[]
                    {
                        new CellBuilder().WithCoordinates(-100, -1).Build()
                    }).Build()
                },
                new[]
                {
                    new ShipBuilder().WithCells(new[]
                    {
                        new CellBuilder().WithCoordinates(-100, -1).Build()
                    }).Build()
                }
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
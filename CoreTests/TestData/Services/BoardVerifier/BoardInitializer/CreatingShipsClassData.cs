using System;
using System.Collections;
using System.Collections.Generic;
using Core.Model;
using CoreTests.Utils;

namespace CoreTests.TestData.Services.BoardVerifier.BoardInitializer
{
    public class CreatingShipsClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new HashSet<ShipConfiguration>(new[]
                {
                    new ShipConfigurationBuilder().WithName("Name1").WithShipSize(1).WithShipsNumber(1).Build(),
                    new ShipConfigurationBuilder().WithName("Name2").WithShipSize(2).WithShipsNumber(3).Build()
                }),
                new[]
                {
                    new CellBuilder().Build()
                },
                new[]
                {
                    new ShipBuilder().WithName("Name1")
                                     .WithCells(new[] {new CellBuilder().Build()})
                                     .Build(),
                    new ShipBuilder().WithName("Name2")
                                     .WithCells(new[] {new CellBuilder().Build()})
                                     .Build(),
                    new ShipBuilder().WithName("Name2")
                                     .WithCells(new[] {new CellBuilder().Build()})
                                     .Build(),
                    new ShipBuilder().WithName("Name2")
                                     .WithCells(new[] {new CellBuilder().Build()})
                                     .Build()
                }
            };

            yield return new object[]
            {
                new HashSet<ShipConfiguration>(new[]
                {
                    new ShipConfigurationBuilder().WithName("111").WithShipSize(1).WithShipsNumber(0).Build(),
                    new ShipConfigurationBuilder().WithName("222").WithShipSize(2).WithShipsNumber(1).Build()
                }),
                new[]
                {
                    new CellBuilder().Build()
                },
                new[]
                {
                    new ShipBuilder().WithName("222")
                                     .WithCells(new[] {new CellBuilder().Build()})
                                     .Build()
                }
            };

            yield return new object[]
            {
                new HashSet<ShipConfiguration>(new[]
                {
                    new ShipConfigurationBuilder().WithName("111").WithShipSize(1).WithShipsNumber(0).Build()
                }),
                new[]
                {
                    new CellBuilder().Build()
                },
                Array.Empty<Ship>()
            };
            yield return new object[]
            {
                new HashSet<ShipConfiguration>(new[]
                {
                    new ShipConfigurationBuilder().WithName("xx").WithShipSize(1).WithShipsNumber(1).Build(),
                    new ShipConfigurationBuilder().WithName("yy").WithShipSize(2).WithShipsNumber(3).Build(),
                    new ShipConfigurationBuilder().WithName("zz").WithShipSize(2).WithShipsNumber(2).Build()
                }),
                new[]
                {
                    new CellBuilder().WithCoordinates(4, 2).Build(),
                    new CellBuilder().WithCoordinates(6, 1).Build()
                },
                new[]
                {
                    new ShipBuilder().WithName("xx")
                                     .WithCells(new[]
                                     {
                                         new CellBuilder().WithCoordinates(4, 2).Build(),
                                         new CellBuilder().WithCoordinates(6, 1).Build()
                                     })
                                     .Build(),
                    new ShipBuilder().WithName("yy")
                                     .WithCells(new[]
                                     {
                                         new CellBuilder().WithCoordinates(4, 2).Build(),
                                         new CellBuilder().WithCoordinates(6, 1).Build()
                                     })
                                     .Build(),
                    new ShipBuilder().WithName("yy")
                                     .WithCells(new[]
                                     {
                                         new CellBuilder().WithCoordinates(4, 2).Build(),
                                         new CellBuilder().WithCoordinates(6, 1).Build()
                                     })
                                     .Build(),
                    new ShipBuilder().WithName("yy")
                                     .WithCells(new[]
                                     {
                                         new CellBuilder().WithCoordinates(4, 2).Build(),
                                         new CellBuilder().WithCoordinates(6, 1).Build()
                                     })
                                     .Build(),
                    new ShipBuilder().WithName("zz")
                                     .WithCells(new[]
                                     {
                                         new CellBuilder().WithCoordinates(4, 2).Build(),
                                         new CellBuilder().WithCoordinates(6, 1).Build()
                                     })
                                     .Build(),
                    new ShipBuilder().WithName("zz")
                                     .WithCells(new[]
                                     {
                                         new CellBuilder().WithCoordinates(4, 2).Build(),
                                         new CellBuilder().WithCoordinates(6, 1).Build()
                                     })
                                     .Build()
                }
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
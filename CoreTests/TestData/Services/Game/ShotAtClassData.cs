using System.Collections;
using System.Collections.Generic;
using Core.Model;
using CoreTests.TestUtils;

namespace CoreTests.TestData.Services.Game
{
    public class ShotAtClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new BoardBuilder()
                    .WithShips(new[]
                    {
                        new ShipBuilder().WithName("firstShip")
                                         .WithCells(new[]
                                         {
                                             new CellBuilder().WithCoordinates(2, 2).Build()
                                         }).Build()
                    }).Build(),
                new CellBuilder().WithCoordinates(2, 2).Build(),
                new GameMoveResult(
                    new BoardBuilder()
                        .WithShips(new[]
                        {
                            new ShipBuilder().WithName("firstShip")
                                             .WithCells(new[]
                                             {
                                                 new CellBuilder().WithCoordinates(2, 2).Build()
                                             }).Build()
                        }).Build(),
                    new ShipBuilder().WithName("firstShip")
                                     .WithCells(new[]
                                     {
                                         new CellBuilder().WithCoordinates(2, 2).Build()
                                     }).Build())
            };
            yield return new object[]
            {
                new BoardBuilder()
                    .WithShips(new[]
                    {
                        new ShipBuilder().WithName("xxx")
                                         .WithCells(new[]
                                         {
                                             new CellBuilder().WithCoordinates(2, 2).WithIsShot(true).Build(),
                                             new CellBuilder().WithCoordinates(4, 2).WithIsShot(true).Build(),
                                             new CellBuilder().WithCoordinates(3, 2).WithIsShot(true).Build()
                                         }).Build(),
                        new ShipBuilder().WithName("yyy")
                                         .WithCells(new[]
                                         {
                                             new CellBuilder().WithCoordinates(24, 6).Build(),
                                             new CellBuilder().WithCoordinates(24, 7).WithIsShot(true).Build()
                                         }).Build()
                    }).Build(),
                new CellBuilder().WithCoordinates(24, 6).Build(),
                new GameMoveResult(
                    new BoardBuilder()
                        .WithShips(new[]
                        {
                            new ShipBuilder().WithName("xxx")
                                             .WithCells(new[]
                                             {
                                                 new CellBuilder().WithCoordinates(2, 2).WithIsShot(true).Build(),
                                                 new CellBuilder().WithCoordinates(4, 2).WithIsShot(true).Build(),
                                                 new CellBuilder().WithCoordinates(3, 2).WithIsShot(true).Build()
                                             }).Build(),
                            new ShipBuilder().WithName("yyy")
                                             .WithCells(new[]
                                             {
                                                 new CellBuilder().WithCoordinates(24, 6).Build(),
                                                 new CellBuilder().WithCoordinates(24, 7).WithIsShot(true).Build()
                                             }).Build()
                        }).Build(),
                    new ShipBuilder().WithName("yyy")
                                     .WithCells(new[]
                                     {
                                         new CellBuilder().WithCoordinates(24, 6).Build(),
                                         new CellBuilder().WithCoordinates(24, 7).WithIsShot(true).Build()
                                     }).Build())
            };


            yield return new object[]
            {
                new BoardBuilder()
                    .WithShips(new[]
                    {
                        new ShipBuilder().WithName("xxx")
                                         .WithCells(new[]
                                         {
                                             new CellBuilder().WithCoordinates(0, 0).WithIsShot(true).Build()
                                         }).Build()
                    })
                    .WithCellsWithoutShips(new[] {new CellBuilder().WithCoordinates(1, 0).Build()})
                    .Build(),
                new CellBuilder().WithCoordinates(1, 0).Build(),
                new GameMoveResult(
                    new BoardBuilder()
                        .WithShips(new[]
                        {
                            new ShipBuilder().WithName("xxx")
                                             .WithCells(new[]
                                             {
                                                 new CellBuilder().WithCoordinates(0, 0).WithIsShot(true).Build()
                                             }).Build()
                        })
                        .WithCellsWithoutShips(new[] {new CellBuilder().WithCoordinates(1, 0).WithIsShot(true).Build()})
                        .Build(),
                    null)
            };
            
            yield return new object[]
            {
                new BoardBuilder()
                    .WithShips(new[]
                    {
                        new ShipBuilder().WithName("qwerty!!")
                                         .WithCells(new[]
                                         {
                                             new CellBuilder().WithCoordinates(0, 0).WithIsShot(true).Build()
                                         }).Build()
                    })
                    .WithCellsWithoutShips(new[]
                    {
                        new CellBuilder().WithCoordinates(1, 0).Build(),
                        new CellBuilder().WithCoordinates(2, 0).Build()
                    })
                    .Build(),
                new CellBuilder().WithCoordinates(1, 0).Build(),
                new GameMoveResult(
                    new BoardBuilder()
                        .WithShips(new[]
                        {
                            new ShipBuilder().WithName("qwerty!!")
                                             .WithCells(new[]
                                             {
                                                 new CellBuilder().WithCoordinates(0, 0).WithIsShot(true).Build()
                                             }).Build()
                        })
                        .WithCellsWithoutShips(new[]
                        {
                            new CellBuilder().WithCoordinates(1, 0).WithIsShot(true).Build(),
                            new CellBuilder().WithCoordinates(2, 0).Build()
                        })
                        .Build(),
                    null)
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
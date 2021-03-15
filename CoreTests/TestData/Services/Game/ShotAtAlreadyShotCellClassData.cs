using System.Collections;
using System.Collections.Generic;
using Core.Model;
using CoreTests.Utils;

namespace CoreTests.TestData.Services.Game
{
    public class ShotAtAlreadyShotCellClassData : IEnumerable<object[]>
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
                                             new CellBuilder().WithIsShot(true).WithCoordinates(2, 2).Build()
                                         }).Build()
                    }).Build(),
                new CellBuilder().WithCoordinates(2, 2).Build()
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
                                             new CellBuilder().WithCoordinates(24, 7).WithIsShot(true).Build(),
                                         }).Build()
                    }).Build(),
                new CellBuilder().WithCoordinates(24, 7).Build()
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
using System.Collections;
using System.Collections.Generic;
using Core.Model;
using CoreTests.Utils;

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
                new GameMoveResult(new BoardBuilder()
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
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
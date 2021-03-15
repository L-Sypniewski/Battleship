using System.Collections;
using System.Collections.Generic;
using Core.Model;
using CoreTests.Utils;

namespace CoreTests.TestData.Services.Game
{
    public class NotFinishedGameClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new BoardBuilder()
                    .WithShips(new[]
                    {
                        new ShipBuilder().WithCells(new[]
                        {
                            NotShotCell()
                        }).Build()
                    }).Build()
            };
            yield return new object[]
            {
                new BoardBuilder()
                    .WithShips(new[]
                    {
                        new ShipBuilder().WithCells(new[]
                        {
                            NotShotCell(),
                            ShotCell()
                        }).Build()
                    }).Build()
            };
            yield return new object[]
            {
                new BoardBuilder()
                    .WithShips(new[]
                    {
                        new ShipBuilder().WithCells(new[]
                        {
                            NotShotCell(),
                            NotShotCell()
                        }).Build(),
                        new ShipBuilder().WithCells(new[]
                        {
                            ShotCell(),
                            ShotCell(),
                            ShotCell()
                        }).Build()
                    }).Build()
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        private static Cell ShotCell() => new CellBuilder().WithIsShot(true).Build();
        private static Cell NotShotCell() => new CellBuilder().WithIsShot(false).Build();
    }
}
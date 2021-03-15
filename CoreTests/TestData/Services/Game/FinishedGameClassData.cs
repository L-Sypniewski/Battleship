using System.Collections;
using System.Collections.Generic;
using Core.Model;
using CoreTests.Utils;

namespace CoreTests.TestData.Services.Game
{
    public class FinishedGameClassData : IEnumerable<object[]>
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
                            ShotCell(),
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
                            ShotCell(),
                            ShotCell()
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
    }
}
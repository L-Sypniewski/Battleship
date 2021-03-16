using System.Collections;
using System.Collections.Generic;
using Core.Model;
using CoreTests.TestUtils;

namespace CoreTests.TestData.Services.BoardVerifier
{
    public class ShipsWithinBoardBoundsCoordinatesClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new BoardSize(10, 10),
                new[]
                {
                    new CellBuilder().WithCoordinates(0, 0).Build(),
                    new CellBuilder().WithCoordinates(2, 1).Build()
                },
                true
            };
            yield return new object[]
            {
                new BoardSize(100, 75),
                new[]
                {
                    new CellBuilder().WithCoordinates(100, 75).Build()
                },
                false
            };
            yield return new object[]
            {
                new BoardSize(75, 100),
                new[]
                {
                    new CellBuilder().WithCoordinates(74, 99).Build()
                },
                true
            };
            yield return new object[]
            {
                new BoardSize(75, 100),
                new[]
                {
                    new CellBuilder().WithCoordinates(100, 75).Build()
                },
                false
            };
            yield return new object[]
            {
                new BoardSize(10, 10),
                new[]
                {
                    new CellBuilder().WithCoordinates(10, 10).Build(),
                    new CellBuilder().WithCoordinates(10, 10).Build()
                },
                false
            };
            yield return new object[]
            {
                new BoardSize(10, 10),
                new[]
                {
                    new CellBuilder().WithCoordinates(9, 9).Build(),
                    new CellBuilder().WithCoordinates(9, 9).Build()
                },
                true
            };
            yield return new object[]
            {
                new BoardSize(10, 10),
                new[]
                {
                    new CellBuilder().WithCoordinates(25, 25).Build(),
                    new CellBuilder().WithCoordinates(25, 25).Build()
                },
                false
            };
            yield return new object[]
            {
                new BoardSize(25, 25),
                new[]
                {
                    new CellBuilder().WithCoordinates(25, 25).Build(),
                    new CellBuilder().WithCoordinates(25, 25).Build()
                },
                false
            };
            yield return new object[]
            {
                new BoardSize(25, 26),
                new[]
                {
                    new CellBuilder().WithCoordinates(24, 25).Build(),
                    new CellBuilder().WithCoordinates(23, 25).Build()
                },
                true
            };
            yield return new object[]
            {
                new BoardSize(10, 10),
                new[]
                {
                    new CellBuilder().WithCoordinates(0, 0).Build(),
                    new CellBuilder().WithCoordinates(2, 11).Build()
                },
                false
            };
            yield return new object[]
            {
                new BoardSize(10, 10),
                new[]
                {
                    new CellBuilder().WithCoordinates(100, 0).Build(),
                    new CellBuilder().WithCoordinates(12, 8).Build()
                },
                false
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
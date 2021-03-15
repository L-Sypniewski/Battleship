using System.Collections;
using System.Collections.Generic;
using CoreTests.Utils;

namespace CoreTests.TestData.Services.GameRulesCellVerifier
{
    public class ValidCellsClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new[] {new CellBuilder().WithCoordinates(0, 0).Build()}
            };
            yield return new object[]
            {
                new[] {new CellBuilder().WithCoordinates(10, 23).Build()}
            };
            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(0, 0).Build(),
                    new CellBuilder().WithCoordinates(0, 1).Build(),
                    new CellBuilder().WithCoordinates(0, 2).Build()
                }
            };
            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(0, 0).Build(),
                    new CellBuilder().WithCoordinates(0, 1).Build()
                }
            };
            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(0, 0).Build(),
                    new CellBuilder().WithCoordinates(0, 2).Build(),
                    new CellBuilder().WithCoordinates(0, 1).Build()
                }
            };
            yield return new[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(1, 0).Build(),
                    new CellBuilder().WithCoordinates(2, 0).Build(),
                    new CellBuilder().WithCoordinates(3, 0).Build()
                }
            };
            yield return new[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(2, 0).Build(),
                    new CellBuilder().WithCoordinates(1, 0).Build(),
                    new CellBuilder().WithCoordinates(3, 0).Build()
                }
            };
            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(24, 5).Build(),
                    new CellBuilder().WithCoordinates(24, 6).Build(),
                    new CellBuilder().WithCoordinates(24, 7).Build()
                }
            };
            yield return new object[]
            {
                new[]
                {
                    new CellBuilder().WithCoordinates(2, 120).Build(),
                    new CellBuilder().WithCoordinates(2, 121).Build(),
                    new CellBuilder().WithCoordinates(2, 122).Build(),
                    new CellBuilder().WithCoordinates(2, 123).Build()
                }
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
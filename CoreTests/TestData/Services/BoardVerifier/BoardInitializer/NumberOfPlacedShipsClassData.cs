using System.Collections;
using System.Collections.Generic;
using Core.Model;
using CoreTests.Utils;

namespace CoreTests.TestData.Services.BoardVerifier.BoardInitializer
{
    public class NumberOfPlacedShipsClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new HashSet<ShipConfiguration>(new[]
                {
                    new ShipConfigurationBuilder().WithShipSize(1).WithShipsNumber(2).Build(),
                    new ShipConfigurationBuilder().WithShipSize(2).WithShipsNumber(4).Build()
                }),
                6
            };
            yield return new object[]
            {
                new HashSet<ShipConfiguration>(new[]
                {
                    new ShipConfigurationBuilder().WithShipSize(4).WithShipsNumber(2).Build(),
                    new ShipConfigurationBuilder().WithShipSize(1).WithShipsNumber(2).Build()
                }),
                4
            };
            yield return new object[]
            {
                new HashSet<ShipConfiguration>(new[]
                {
                    new ShipConfigurationBuilder().WithShipSize(2).WithShipsNumber(5).Build()
                }),
                5
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
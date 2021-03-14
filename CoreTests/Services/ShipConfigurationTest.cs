using System.Collections.Generic;
using System.Linq;
using Core.Model;
using FluentAssertions;
using Xunit;

namespace CoreTests.Services
{
    public class ShipConfigurationTest
    {
        [Theory(DisplayName = "Two configurations with the same ship size are considered to be equal")]
        [MemberData(nameof(EqualShipConfigurationsData))]
        public void Two_configurations_with_the_same_ship_size_are_considered_to_be_equal(ShipConfiguration[] shipConfigurations)
        {
            var referenceObject = shipConfigurations.First();

            shipConfigurations.Should().AllBeEquivalentTo(referenceObject);
        }


        public static IEnumerable<object[]> EqualShipConfigurationsData()
        {
            return new[]
            {
                new object[]
                {
                    new[] {new ShipConfiguration(1, 1), new ShipConfiguration(1, 2)}
                },
                new object[]
                {
                    new[] {new ShipConfiguration(int.MaxValue, int.MaxValue), new ShipConfiguration(int.MaxValue, int.MinValue)}
                },
                new object[]
                {
                    new[] {new ShipConfiguration(5, 2), new ShipConfiguration(5, -2), new ShipConfiguration(5, 33)}
                }
            };
        }


        [Theory(DisplayName =
            "Two configurations with the same ships number, but different ship size are NOT considered to be equal")]
        [MemberData(nameof(UnequalShipConfigurationsData))]
        public void Two_configurations_with_the_same_ships_number_but_different_ship_size_are_not_considered_to_be_equal(
            ShipConfiguration[] shipConfigurations)
        {
            shipConfigurations.Should().OnlyHaveUniqueItems();
        }


        public static IEnumerable<object[]> UnequalShipConfigurationsData()
        {
            return new[]
            {
                new object[]
                {
                    new[] {new ShipConfiguration(2, 1), new ShipConfiguration(3, 1)}
                },
                new object[]
                {
                    new[] {new ShipConfiguration(int.MaxValue, int.MinValue), new ShipConfiguration(int.MinValue, int.MinValue)}
                },
                new object[]
                {
                    new[] {new ShipConfiguration(5, 2), new ShipConfiguration(-5, 2), new ShipConfiguration(15, 2)}
                }
            };
        }
    }
}
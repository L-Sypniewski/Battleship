using System.Collections.Generic;
using System.Linq;
using Core.Model;
using CoreTests.Utils;
using FluentAssertions;
using Xunit;

namespace CoreTests.Model
{
    public class ShipConfigurationTest
    {
        [Theory(DisplayName = "Two configurations with the same name nad ship size are considered to be equal")]
        [MemberData(nameof(EqualShipConfigurationsData))]
        public void Two_configurations_with_the_same_name_and_ship_size_are_considered_to_be_equal(
            ShipConfiguration[] shipConfigurations)
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
                    new[]
                    {
                        new ShipConfigurationBuilder().WithName("myName").WithShipSize(1).WithShipsNumber(1).Build(),
                        new ShipConfigurationBuilder().WithName("myName").WithShipSize(1).WithShipsNumber(2).Build()
                    }
                },
                new object[]
                {
                    new[]
                    {
                        new ShipConfigurationBuilder().WithName("myAnotherName").WithShipSize(int.MaxValue)
                                                      .WithShipsNumber(int.MaxValue).Build(),
                        new ShipConfigurationBuilder().WithName("myAnotherName").WithShipSize(int.MaxValue)
                                                      .WithShipsNumber(int.MinValue).Build()
                    }
                },
                new object[]
                {
                    new[]
                    {
                        new ShipConfigurationBuilder().WithName("xxx").WithShipSize(5).WithShipsNumber(2).Build(),
                        new ShipConfigurationBuilder().WithName("xxx").WithShipSize(5).WithShipsNumber(-2).Build(),
                        new ShipConfigurationBuilder().WithName("xxx").WithShipSize(5).WithShipsNumber(33).Build()
                    }
                }
            };
        }


        [Theory(DisplayName =
            "Two configurations with the same ships number, but different ship size or name are NOT considered to be equal")]
        [MemberData(nameof(UnequalShipConfigurationsData))]
        public void Two_configurations_with_the_same_ships_number_but_different_ship_size_or_name_are_not_considered_to_be_equal(
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
                    new[]
                    {
                        new ShipConfigurationBuilder().WithShipSize(2).WithShipsNumber(1).Build(),
                        new ShipConfigurationBuilder().WithShipSize(3).WithShipsNumber(1).Build()
                    }
                },
                new object[]
                {
                    new[]
                    {
                        new ShipConfigurationBuilder().WithShipSize(int.MaxValue).WithShipsNumber(int.MinValue).Build(),
                        new ShipConfigurationBuilder().WithShipSize(int.MinValue).WithShipsNumber(int.MinValue).Build()
                    }
                },
                new object[]
                {
                    new[]
                    {
                        new ShipConfigurationBuilder().WithShipSize(5).WithShipsNumber(2).Build(),
                        new ShipConfigurationBuilder().WithShipSize(-5).WithShipsNumber(2).Build(),
                        new ShipConfigurationBuilder().WithShipSize(15).WithShipsNumber(2).Build()
                    }
                },
                new object[]
                {
                    new[]
                    {
                        new ShipConfigurationBuilder().WithName("xx").WithShipSize(5).WithShipsNumber(2).Build(),
                        new ShipConfigurationBuilder().WithName("11").WithShipSize(5).WithShipsNumber(2).Build()
                    }
                },
                new object[]
                {
                    new[]
                    {
                        new ShipConfigurationBuilder().WithName("xx").WithShipSize(5).WithShipsNumber(2).Build(),
                        new ShipConfigurationBuilder().WithName("11").WithShipSize(10).WithShipsNumber(2).Build()
                    }
                }
            };
        }
    }
}
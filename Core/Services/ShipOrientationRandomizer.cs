using System;
using Core.Model;

namespace Core.Services
{
    public sealed class ShipOrientationRandomizer : IShipOrientationRandomizer
    {
        private readonly Random _random = new();
        private readonly ShipOrientation[] _shipOrientationValues = Enum.GetValues<ShipOrientation>();


        public ShipOrientation GetOrientation() =>
            ( ShipOrientation ) _shipOrientationValues.GetValue(_random.Next(_shipOrientationValues.Length))!;
    }
}
using Core.Model;

namespace CoreTests.Utils
{
    public class ShipConfigurationBuilder
    {
        private string _name = "";
        private int _shipsNumber;
        private int _shipSize;


        public ShipConfigurationBuilder WithName(string name)
        {
            _name = name;
            return this;
        }


        public ShipConfigurationBuilder WithShipSize(int shipSize)
        {
            _shipSize = shipSize;
            return this;
        }


        public ShipConfigurationBuilder WithShipsNumber(int shipsNumber)
        {
            _shipsNumber = shipsNumber;
            return this;
        }


        public ShipConfiguration Build()
        {
            return new(_name, _shipSize, _shipsNumber);
        }
    }
}
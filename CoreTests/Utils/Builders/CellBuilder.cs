using Core.Model;

namespace CoreTests.Utils
{
    public class CellBuilder
    {
        private bool _isShot;
        private int _xCoordinate;
        private int _yCoordinate;


        public CellBuilder()
        {
            _yCoordinate = 0;
        }


        public CellBuilder WithIsShot(bool isShot)
        {
            _isShot = isShot;
            return this;
        }


        public CellBuilder WithCoordinates(int xCoordinate, int yCoordinate)
        {
            _xCoordinate = xCoordinate;
            _yCoordinate = yCoordinate;
            return this;
        }


        public Cell Build()
        {
            return new(_xCoordinate, _yCoordinate, _isShot);
        }
    }
}
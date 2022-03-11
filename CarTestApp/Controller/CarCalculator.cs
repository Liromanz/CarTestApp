using CarTestApp.Interface;
using CarTestApp.Model.Base;

namespace CarTestApp.Controller
{
    public class CarCalculator : ICarCalculator
    {
        private readonly Car _car;

        public CarCalculator(Car car)
        {
            _car = car;
        }

        public double FullTankDistance()
        {
            return _car.FuelTank / (_car.FuelUse / 100);
        }

        public double CurrentTankDistance(double currentTank)
        {
            return currentTank / (_car.FuelUse / 100);
        }

        public double ReserveDistance(int passengerCount = 0, double cargoWeight = 0)
        {
            var distance = FullTankDistance();
            return distance - (distance / 100 * 6) * passengerCount - (distance / 100 * 12) * cargoWeight;
        }

        public (double literToRide, double timeRide) TimeByDistance(double currentTank, double distance)
        {
            return (_car.FuelUse * distance / 100, distance/_car.Speed);
        }
    }
}

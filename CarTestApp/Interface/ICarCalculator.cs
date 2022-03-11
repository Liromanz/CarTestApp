using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTestApp.Interface
{
    public interface ICarCalculator
    {
        double FullTankDistance();
        double CurrentTankDistance(double currentTank);
        double ReserveDistance(int passengerCount, double cargoWeight);
        (double literToRide, double timeRide) TimeByDistance(double currentTank, double distance);
    }
}

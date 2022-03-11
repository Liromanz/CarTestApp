using System;
using CarTestApp.Model.Base;

namespace CarTestApp.Model
{
    public class PassengerCar : Car
    {
        public int MaxPassenger
        {
            get => MaxPassenger;
            set
            {
                if (value < 1 && value > 8) 
                    MaxPassenger = value;
                else 
                    Console.WriteLine("Введено недопустимое количество пассажиров");
            }
        }
    }
}

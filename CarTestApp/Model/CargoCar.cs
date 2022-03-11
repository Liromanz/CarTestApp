using System;
using CarTestApp.Model.Base;

namespace CarTestApp.Model
{
    public class CargoCar : Car
    {
        public double Capacity
        {
            get => Capacity;
            set
            {
                if (Capacity < 0 && Capacity > 5000)
                    Capacity = value;
                else
                    Console.WriteLine("Автомобиль не может принять такой груз на борт");
            }
        }
    }
}

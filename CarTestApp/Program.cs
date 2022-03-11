using System;
using System.Linq;
using CarTestApp.Controller;
using CarTestApp.Model;
using CarTestApp.Model.Base;

namespace CarTestApp
{
    class Program
    {
        private static Car _car = new Car();
        static void Main(string[] args)
        {
            Console.WriteLine("Здравствуйте! Введите параметры машины.");
            Console.Write("Выберите тип транспортного средства, введя название: Легковой, Грузовой, Спортивный: ");
            var type = Console.ReadLine();
            while (Enum.GetNames(typeof(CarType)).All(x => x != type))
            {
                Console.Write("Такого типа ТС нет. Введите одно из перечисленных вариантов: ");
                type = Console.ReadLine();
            }

            InitializeBasedOnType(type);

            _car.Type = (CarType)Enum.Parse(typeof(CarType), type);

            Console.Write("Введите средний расход топлива в литрах на 100 км: ");
            double fuelUse;
            CheckDouble(out fuelUse);
            _car.FuelUse = fuelUse;

            Console.Write("Введите объем топливного бака в литрах: ");
            int fuelTank;
            CheckInt(out fuelTank);
            _car.FuelTank = fuelTank;

            Console.Write("Введите среднюю скорость в км/ч: ");
            double speed;
            CheckDouble(out speed);

            _car.Speed = speed;

            Console.WriteLine("Спасибо! Теперь, выберите один из расчетов, вписывая число.");
            Console.WriteLine("1. Расчет сколько автомобиль может проехать на полном баке топлива");
            Console.WriteLine("2. Расчет сколько автомобиль может проехать на остаточном количестве топлива в баке");
            Console.WriteLine("3. Расчет текущей информации о состоянии запаса хода в зависимости от пассажиров и груза");
            Console.WriteLine("4. Расчет времени и количества топлива на расстояние");
            Console.WriteLine("Для выхода впишите слово \"Стоп\"");

            var calculator = new CarCalculator(_car);
            var method = Console.ReadLine();
            while (method != "Стоп")
            {
                switch (Convert.ToInt16(method))
                {
                    case 1:
                        Console.WriteLine($"Автомобиль может проехать {calculator.FullTankDistance()} км");
                        break;
                    case 2:
                        Console.Write("Введите текущее количество топлива: ");
                        double currentTank;
                        CheckDouble(out currentTank);

                        Console.WriteLine($"Автомобиль может проехать {calculator.CurrentTankDistance(currentTank)} км");
                        break;
                    case 3:
                        if (_car.GetType() == typeof(PassengerCar))
                        {
                            Console.Write("Введите количество перевозимых пассажиров: ");
                            int currentPassengers;
                            CheckInt(out currentPassengers);

                            Console.WriteLine($"Запас хода автомобиля {calculator.ReserveDistance(passengerCount: currentPassengers)} км");
                        }
                        else if (_car.GetType() == typeof(CargoCar))
                        {
                            Console.Write("Введите грузоподьемность автомобиля: ");
                            double capacity;
                            CheckDouble(out capacity);

                            Console.WriteLine($"Запас хода автомобиля {calculator.ReserveDistance(cargoWeight: capacity)} км");
                        }
                        else
                            Console.WriteLine("Метод не поддерживается для спорткаров");
                        break;
                    case 4:
                        Console.Write("Введите текущее количество топлива: ");
                        double curTank;
                        CheckDouble(out curTank);

                        Console.Write("Введите расстояние которое необходимо пройти: ");
                        double distance;
                        CheckDouble(out distance);

                        var timeAndLiter = calculator.TimeByDistance(curTank, distance);
                        Console.WriteLine($"Автомобиль проедет это расстояние за {timeAndLiter.timeRide} часов");
                        Console.WriteLine($"Автомобиль потратит {timeAndLiter.literToRide} литров на путь");
                        break;
                }
                Console.WriteLine($"Спасибо за использование программы! Нажмите любую клавишу для закрытия");
                method = Console.ReadLine();
            }
            Console.ReadKey();
        }

        private static void InitializeBasedOnType(string type)
        {
            if (type == Enum.GetName(typeof(CarType), CarType.Легковой))
                _car = new PassengerCar();
            else if (type == Enum.GetName(typeof(CarType), CarType.Грузовой))
                _car = new CargoCar();
            else
                _car = new SportCar();
        }

        private static void CheckDouble(out double doubleValue)
        {
            while (!double.TryParse(Console.ReadLine(), out doubleValue))
                Console.Write("Значение не является числом. Попробуйте еще раз: ");
        }

        private static void CheckInt(out int intValue)
        {
            while (!int.TryParse(Console.ReadLine(), out intValue))
                Console.Write("Значение не является числом. Попробуйте еще раз: ");
        }
    }
}

using System;

namespace NeedForSpeed
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            SportCar sportCar = new SportCar(150, 100);

            Console.WriteLine($"FuelBefore: {sportCar.Fuel}");

            sportCar.Drive(5);

            Console.WriteLine($"FuelAfter: {sportCar.Fuel}");
        }
    }
}

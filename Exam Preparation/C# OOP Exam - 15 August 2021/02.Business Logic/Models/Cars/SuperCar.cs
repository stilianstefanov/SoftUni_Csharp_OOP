namespace CarRacing.Models.Cars
{
    public class SuperCar : Car
    {
        private const double InitialFuel = 80;
        private const double InitialFuelConsumption = 10;

        public SuperCar(string make, string model, string VIN, int horsePower)
            : base(make, model, VIN, horsePower, InitialFuel, InitialFuelConsumption)
        {
        }
    }
}

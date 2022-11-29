using System;

namespace CarRacing.Models.Cars
{
    public class TunedCar : Car
    {
        private const double InitialFuel = 65;
        private const double InitialFuelConsumption = 7.5;

        public TunedCar(string make, string model, string VIN, int horsePower)
            : base(make, model, VIN, horsePower, InitialFuel, InitialFuelConsumption)
        {
        }

        public override void Drive()
        {
            base.Drive();

            double horsePowerLeft = Math.Round(HorsePower - (HorsePower * 0.03));
            HorsePower = (int)horsePowerLeft;
        }
    }
}


using System;

namespace Vehicles.Models
{
    public class Truck : Vehicle
    {
        private const double ADDITIONAL_CONSUMPTION = 1.6;
        private const double REFUEL_MAX_PERCENTAGE = 0.95;
        public Truck(double fuelQuantity, double fuelConsumption, double capacity) 
            : base(fuelQuantity, fuelConsumption + ADDITIONAL_CONSUMPTION, capacity)
        {            
        }

        public override void Refuel(double fuelAmount)
        {
            double actualFuelAmmount = fuelAmount * REFUEL_MAX_PERCENTAGE;
            if (fuelAmount <= 0)
            {
                throw new ArgumentException("Fuel must be a positive number");
            }
            else if (this.FuelQuantity + actualFuelAmmount > this.Capacity)
            {
                throw new InvalidOperationException($"Cannot fit {fuelAmount} fuel in the tank");
            }
            this.FuelQuantity += actualFuelAmmount;
        }
       
    }
}

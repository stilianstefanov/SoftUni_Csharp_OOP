
namespace Vehicles.Models
{
    public class Truck : Vehicle
    {
        private const double ADDITIONAL_CONSUMPTION = 1.6;
        private const double REFUEL_MAX_PERCENTAGE = 0.95;
        public Truck(double fuelQuantity, double fuelConsumption) : base(fuelQuantity, fuelConsumption + ADDITIONAL_CONSUMPTION)
        {            
        }

        public override void Refuel(double fuelAmount)
            => this.FuelQuantity += (fuelAmount * REFUEL_MAX_PERCENTAGE);
       
    }
}

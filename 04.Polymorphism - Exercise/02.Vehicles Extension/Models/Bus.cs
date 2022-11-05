
namespace Vehicles.Models
{
    public class Bus : Vehicle
    {
        private const double ADDITIONAL_CONSUMPTION = 1.4;
        public Bus(double fuelQuantity, double fuelConsumption, double capacity) 
            : base(fuelQuantity, fuelConsumption + ADDITIONAL_CONSUMPTION, capacity)
        {
        }

        public string DriveEmpty(double distance)
        {
            double fuelLeft = this.FuelQuantity - (distance * (FuelConsumption - ADDITIONAL_CONSUMPTION));

            if (fuelLeft < 0)
            {
                return $"{this.GetType().Name} needs refueling";
            }
            this.FuelQuantity = fuelLeft;

            return $"{this.GetType().Name} travelled {distance} km";
        }
    }
}

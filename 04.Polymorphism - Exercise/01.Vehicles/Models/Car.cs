
namespace Vehicles.Models
{
    public class Car : Vehicle
    {
        private const double ADDITIONAL_CONSUMPTION = 0.9;
        public Car(double fuelQuantity, double fuelConsumption) : base(fuelQuantity, fuelConsumption + ADDITIONAL_CONSUMPTION)

        {            
        }
    }
}

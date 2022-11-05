

namespace Vehicles.Models
{
    using Vehicles.Models.Contracts;

    public abstract class Vehicle : IVehicle
    {
        protected double additionalConsumption;

        private double fuelQuantity;
        private double fuelConsumption;

        protected Vehicle(double fuelQuantity, double fuelConsumption)
        {
            this.FuelQuantity = fuelQuantity;
            this.fuelConsumption = fuelConsumption;
        }
        public double FuelQuantity
        {
            get { return fuelQuantity; } 
            protected set { fuelQuantity = value; } 
        }
        public string Drive(double distance)
        {
            double fuelLeft = this.FuelQuantity - (distance * fuelConsumption);

            if (fuelLeft < 0)
            {
                return $"{this.GetType().Name} needs refueling";
            }
            this.FuelQuantity = fuelLeft;

            return $"{this.GetType().Name} travelled {distance} km";
        }

        public virtual void Refuel(double fuelAmount)
            => this.FuelQuantity += fuelAmount;
               
        public override string ToString()
            => $"{this.GetType().Name}: {this.FuelQuantity:f2}";
       
    }
}

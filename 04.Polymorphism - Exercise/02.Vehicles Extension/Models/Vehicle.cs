

namespace Vehicles.Models
{
    using System;
    using Vehicles.Models.Contracts;

    public abstract class Vehicle : IVehicle
    {
        protected double additionalConsumption;

        private double fuelQuantity;
        private double fuelConsumption;
        private double capacity;

        protected Vehicle(double fuelQuantity, double fuelConsumption, double capacity)
        {
            this.Capacity = capacity;
            this.FuelQuantity = fuelQuantity;
            this.fuelConsumption = fuelConsumption;
        }
        
        public double FuelConsumption 
        {
            get { return fuelConsumption; } 
            private set { fuelConsumption = value; }
        }

        public double FuelQuantity
        {
            get { return fuelQuantity; } 
            protected set 
            { 
                if (value > this.Capacity)
                    fuelQuantity = 0;
                else
                fuelQuantity = value; 
            } 
        }
        
        public double Capacity
        {
            get { return capacity; }
            protected set { capacity = value; }
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
        {
            if (fuelAmount <= 0)
            {
                throw new ArgumentException("Fuel must be a positive number");
            }
            else if (this.FuelQuantity + fuelAmount > this.Capacity)
            {
                throw new InvalidOperationException($"Cannot fit {fuelAmount} fuel in the tank");
            }
            this.FuelQuantity += fuelAmount;
        }
               
        public override string ToString()
            => $"{this.GetType().Name}: {this.FuelQuantity:f2}";

    }
}

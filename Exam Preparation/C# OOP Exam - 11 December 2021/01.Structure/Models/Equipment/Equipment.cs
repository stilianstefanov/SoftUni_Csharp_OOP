namespace Gym.Models.Equipment
{
    using Contracts;


    public abstract class Equipment : IEquipment
    {
        private double weight;
        private decimal price;

        protected Equipment(double weight, decimal price)
        {
            Weight= weight;
            Price= price;
        }

        public double Weight
        {
            get => weight;
            private set { weight = value; }
        }

        public decimal Price
        {
            get => price;
            private set { price = value; }
        }
    }
}

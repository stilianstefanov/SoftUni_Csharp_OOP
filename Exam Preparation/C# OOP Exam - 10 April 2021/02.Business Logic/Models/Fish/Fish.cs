namespace AquaShop.Models.Fish
{
    using System;
    using Utilities.Messages;
    using Contracts;
    

    public abstract class Fish : IFish
    {
        private string name;
        private string species;
        private int size;
        private decimal price;

        protected Fish(string name, string species, decimal price)
        {
            Name= name;
            Species= species;
            Price= price;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidFishName);
                }
                name = value;
            }
        }

        public string Species
        {
            get => species;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidFishSpecies);
                }
                species = value;
            }
        }

        public int Size
        {
            get => size;
            protected set => size = value;
        }

        public decimal Price
        {
            get => price;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidFishPrice);
                }
                price = value;
            }
        }

        public abstract void Eat();   
    }
}

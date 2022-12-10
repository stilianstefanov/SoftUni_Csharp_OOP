namespace ChristmasPastryShop.Models.Cocktails
{
    using System;

    using Contracts;
    using Utilities.Messages;

    public abstract class Cocktail : ICocktail
    {
        private string name;
        private string size;
        private double price;

        protected Cocktail(string cocktailName, string size, double price)
        {
            Name = cocktailName;
            Size = size;
            Price = price;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }
                name = value;
            }
        }

        public string Size
        {
            get => size;
            private set
            {
                size = value;
            }
        }

        public double Price
        {
            get => price;
            private set
            {
                if (size == "Large")
                {
                    price = value;
                }
                else if (size == "Middle")
                {
                    price = ((double)2 / 3) * value;
                }
                else if (size == "Small")
                {
                    price = ((double)1 / 3) * value;
                }
            }
        }

        public override string ToString()
            => $"{Name} ({Size}) - {Price:F2} lv";
    }
}



namespace PlanetWars.Models.Weapons
{
    using System;

    using Contracts;
    using Utilities.Messages;

    public abstract class Weapon : IWeapon
    {
        private double price;
        private int destructionLevel;

        protected Weapon(int destructionLevel, double price)
        {
            this.Price= price;
            this.DestructionLevel= destructionLevel;
        }

        public double Price
        {
            get => price;
            private set
            {
                price = value;
            }
        }

        public int DestructionLevel
        {
            get => destructionLevel;
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(ExceptionMessages.TooLowDestructionLevel);
                }
                if (value > 10)
                {
                    throw new ArgumentException(ExceptionMessages.TooHighDestructionLevel);
                }
                destructionLevel = value;
            }
        }
    }
}

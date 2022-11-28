namespace SpaceStation.Models.Astronauts
{
    using System;

    using Contracts;
    using Models.Bags.Contracts;
    using Models.Bags;
    using Utilities.Messages;

    public abstract class Astronaut : IAstronaut
    {
        private string name;
        private double oxygen;
        private IBag bag;

        public Astronaut(string name, double oxygen)
        {
            this.Name= name;
            this.Oxygen= oxygen;
            bag = new Backpack();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidAstronautName);
                }
                name = value;
            }
        }

        public double Oxygen
        {
            get => oxygen;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidOxygen);
                }
                oxygen = value;
            }
        }

        public bool CanBreath => oxygen > 0;

        public IBag Bag
        {
            get => bag;
            private set
            {
                bag = value;
            }
        }

        public virtual void Breath()
        {
            double oxygenLeft = Oxygen - 10;
            if (oxygenLeft < 0)
                Oxygen = 0;
            else
                Oxygen = oxygenLeft;
        }
    }
}

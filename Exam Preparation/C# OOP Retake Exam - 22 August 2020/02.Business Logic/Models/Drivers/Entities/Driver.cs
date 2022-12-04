namespace EasterRaces.Models.Drivers.Entities
{
    using System;

    using Contracts;
    using Cars.Contracts;   
    using Utilities.Messages;

    public class Driver : IDriver
    {
        private string name;
        private ICar car;
        private int numberOfWins;

        public Driver(string name)
        {
            Name = name;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidName, value, 5));
                }
                name = value;
            }
        }

        public ICar Car
        {
            get => car;
            private set => car = value;
        }

        public int NumberOfWins
        {
            get => numberOfWins;
            private set => numberOfWins = value;
        }

        public bool CanParticipate => car != null;

        public void AddCar(ICar car)
        {
            if (car == null)
                throw new ArgumentNullException(ExceptionMessages.CarInvalid); // Valid?

            Car = car;
        }

        public void WinRace()
            => NumberOfWins++;        
    }
}

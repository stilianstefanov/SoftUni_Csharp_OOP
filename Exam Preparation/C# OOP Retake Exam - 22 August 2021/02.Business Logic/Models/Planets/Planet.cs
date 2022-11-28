namespace SpaceStation.Models.Planets
{
    using System;
    using System.Collections.Generic;

    using Contracts;
    using Utilities.Messages;

    public class Planet : IPlanet
    {
        private string name;
        private List<string> items;

        public Planet(string name)
        {
            this.Name= name;
            this.items = new List<string>();
        }

        public ICollection<string> Items => items;

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidPlanetName);
                }
                name = value;
            }
        }
    }
}

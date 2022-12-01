namespace AquaShop.Models.Aquariums
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;

    using Decorations.Contracts;
    using Fish.Contracts;
    using Contracts;
    using Utilities.Messages;


    public abstract class Aquarium : IAquarium
    {
        private string name;
        private int capacity;
        private ICollection<IDecoration> decorations;
        private ICollection<IFish> fish;

        protected Aquarium(string name, int capacity)
        {
            Name= name;
            Capacity= capacity;
            this.decorations= new List<IDecoration>();
            this.fish= new List<IFish>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }
                name = value;
            }
        }

        public int Capacity
        {
            get => capacity;
            private set => capacity = value;
        }

        public int Comfort => decorations.Sum(d => d.Comfort);

        public ICollection<IDecoration> Decorations => decorations;

        public ICollection<IFish> Fish => fish;

        public void AddDecoration(IDecoration decoration)
        {
            this.decorations.Add(decoration);
        }

        public void AddFish(IFish fish)
        {
            if (this.fish.Count >= Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }

            this.fish.Add(fish);
        }

        public void Feed()
        {
            foreach (var fish in this.fish)
            {
                fish.Eat();
            }
        }

        public string GetInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{Name} ({this.GetType().Name}):");

            string fishInTheAquarium = this.fish.Any() ? string.Join(", ", fish.Select(f => f.Name)) : "none";

            sb.AppendLine($"Fish: {fishInTheAquarium}");
            sb.AppendLine($"Decorations: {this.decorations.Count}");
            sb.AppendLine($"Comfort: {Comfort}");

            return sb.ToString().TrimEnd();
        }

        public bool RemoveFish(IFish fish)
        {
            return this.fish.Remove(fish);
        }
    }
}

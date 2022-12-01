namespace AquaShop.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;

    using Models.Decorations.Contracts;
    using Repositories.Contracts;
    using Contracts;    
    using Models.Aquariums.Contracts;
    using Repositories;
    using Models.Aquariums;
    using Utilities.Messages;
    using Models.Decorations;   
    using Models.Fish.Contracts;
    using Models.Fish;
    

    public class Controller : IController
    {
        private IRepository<IDecoration> decorations;
        private ICollection<IAquarium> aquariums;

        public Controller()
        {
            decorations = new DecorationRepository();
            aquariums = new List<IAquarium>();
        }

        public string AddAquarium(string aquariumType, string aquariumName)
        {
            IAquarium newAquarium = null;
            switch (aquariumType)
            {
                case "FreshwaterAquarium":
                    newAquarium = new FreshwaterAquarium(aquariumName);
                    break;
                case "SaltwaterAquarium":
                    newAquarium = new SaltwaterAquarium(aquariumName);
                    break;
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);                   
            }

            aquariums.Add(newAquarium);

            return string.Format(OutputMessages.SuccessfullyAdded, aquariumType);
        }

        public string AddDecoration(string decorationType)
        {
            IDecoration newDecoration = null;
            switch (decorationType)
            {
                case "Ornament":
                    newDecoration = new Ornament();
                    break;
                case "Plant":
                    newDecoration = new Plant();
                    break;
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);                   
            }

            decorations.Add(newDecoration);

            return string.Format(OutputMessages.SuccessfullyAdded, decorationType);
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            var aquarium = aquariums.FirstOrDefault(a => a.Name == aquariumName);

            IFish newFish = null;
            switch (fishType)
            {
                case "FreshwaterFish":
                    {
                        newFish = new FreshwaterFish(fishName, fishSpecies, price);
                        if (aquarium.GetType().Name != "FreshwaterAquarium")
                            return OutputMessages.UnsuitableWater;
                        break;
                    }
                case "SaltwaterFish":
                    {
                        newFish = new SaltwaterFish(fishName, fishSpecies, price);
                        if (aquarium.GetType().Name != "SaltwaterAquarium")
                            return OutputMessages.UnsuitableWater;
                        break;
                    }
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidFishType);                   
            }

            aquarium.AddFish(newFish);

            return string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
        }

        public string CalculateValue(string aquariumName)
        {
            var aquariumToCalc = aquariums.FirstOrDefault(a => a.Name == aquariumName);

            decimal totalValue = aquariumToCalc.Fish.Sum(f => f.Price) + aquariumToCalc.Decorations.Sum(d => d.Price);

            return string.Format(OutputMessages.AquariumValue, aquariumName, totalValue);
        }

        public string FeedFish(string aquariumName)
        {
            var aquariumToFeed = aquariums.FirstOrDefault(a => a.Name == aquariumName);

            aquariumToFeed.Feed();

            return string.Format(OutputMessages.FishFed, aquariumToFeed.Fish.Count);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            var decorationToInsert = decorations.FindByType(decorationType);
            if (decorationToInsert == null)
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentDecoration, decorationType));

            var aquarium = aquariums.FirstOrDefault(a => a.Name == aquariumName);

            aquarium.AddDecoration(decorationToInsert);
            decorations.Remove(decorationToInsert);

            return string.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);
        }

        public string Report()
        {
            var sb = new StringBuilder();

            foreach (var aquarium in aquariums)
            {
                sb.AppendLine(aquarium.GetInfo());
            }

            return sb.ToString().TrimEnd();
        }
    }
}

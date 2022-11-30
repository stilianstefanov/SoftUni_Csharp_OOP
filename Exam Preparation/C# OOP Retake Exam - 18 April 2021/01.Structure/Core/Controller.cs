namespace Easter.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Repositories;
    using Models.Bunnies.Contracts;
    using Models.Eggs.Contracts;
    using Repositories.Contracts;
    using Models.Bunnies;   
    using Utilities.Messages;
    using Models.Dyes;
    using Models.Eggs;   
    using Models.Workshops;
    using Models.Workshops.Contracts;
    

    public class Controller : IController
    {
        private IRepository<IEgg> eggs;
        private IRepository<IBunny> bunnies;

        public Controller()
        {
            eggs = new EggRepository();
            bunnies = new BunnyRepository();
        }


        public string AddBunny(string bunnyType, string bunnyName)
        {
            IBunny newBunny = null;
            switch (bunnyType)
            {
                case "HappyBunny":
                    newBunny = new HappyBunny(bunnyName);
                    break;
                case "SleepyBunny":
                    newBunny = new SleepyBunny(bunnyName);
                    break;
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);                  
            }

            bunnies.Add(newBunny);

            return string.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            var bunnyToAddDye = bunnies.FindByName(bunnyName);
            if (bunnyToAddDye == null)
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);

            var newDye = new Dye(power);
            bunnyToAddDye.AddDye(newDye);

            return string.Format(OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            var newEgg = new Egg(eggName, energyRequired);
            eggs.Add(newEgg);

            return string.Format(OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            var eggToColor = eggs.FindByName(eggName);

            var selectedBunnies = bunnies.Models.OrderByDescending(x => x.Energy)
                .TakeWhile(x => x.Energy >= 50);

            if (!selectedBunnies.Any())
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);

            IWorkshop workshop = new Workshop();

            foreach (var bunny in selectedBunnies)
            {
                workshop.Color(eggToColor, bunny);

                if (bunny.Energy == 0)
                    bunnies.Remove(bunny);
            }

            if (eggToColor.IsDone())
                return string.Format(OutputMessages.EggIsDone, eggToColor.Name);
            else
                return string.Format(OutputMessages.EggIsNotDone, eggToColor.Name);
        }

        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{eggs.Models.Count(e => e.IsDone())} eggs are done!");
            sb.AppendLine("Bunnies info:");

            foreach (var bunny in bunnies.Models)
            {
                sb.AppendLine($"Name: {bunny.Name}");
                sb.AppendLine($"Energy: {bunny.Energy}");
                sb.AppendLine($"Dyes: {bunny.Dyes.Count(d => !d.IsFinished())} not finished");
            }

            return sb.ToString().TrimEnd();
        }
    }
}

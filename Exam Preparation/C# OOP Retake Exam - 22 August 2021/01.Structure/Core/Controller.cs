namespace SpaceStation.Core
{
    using System;
    using System.Text;
    using System.Linq;

    using Contracts;
    using Models.Astronauts.Contracts;
    using Repositories.Contracts;
    using Models.Planets.Contracts;
    using Repositories;
    using Models.Astronauts;
    using Utilities.Messages;
    using Models.Planets;    
    using Models.Mission;
    

    public class Controller : IController
    {
        private IRepository<IAstronaut> astronauts;
        private IRepository<IPlanet> planets;
        private int exploredPlanetsCount = 0;

        public Controller()
        {
            astronauts = new AstronautRepository();
            planets = new PlanetRepository();
        }

        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut newAstro = null;
            switch (type)
            {
                case "Biologist":
                    newAstro = new Biologist(astronautName);
                    break;
                case "Geodesist":
                    newAstro = new Geodesist(astronautName);
                    break;
                case "Meteorologist":
                    newAstro = new Meteorologist(astronautName);
                    break;
                default:
                    throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);                    
            }

            astronauts.Add(newAstro);

            return string.Format(OutputMessages.AstronautAdded, type, astronautName);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            var planet = new Planet(planetName);

            foreach (var item in items)
            {
                planet.Items.Add(item);
            }

            planets.Add(planet);

            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string ExplorePlanet(string planetName)
        {
            var astronautsToExplore = astronauts.Models.Where(m => m.Oxygen > 60).ToList();

            if (astronautsToExplore.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }

            var planetToExplore = planets.FindByName(planetName);

            Mission mission = new Mission();
            mission.Explore(planetToExplore, astronautsToExplore);
            exploredPlanetsCount++;

            int deadAstronauts = astronautsToExplore.Where(a => a.Oxygen == 0).Count();

            return string.Format(OutputMessages.PlanetExplored, planetName, deadAstronauts);
        }

        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{exploredPlanetsCount} planets were explored!");
            sb.AppendLine("Astronauts info:");

            foreach (var astronaut in astronauts.Models)
            {                
                string bagItems = astronaut.Bag.Items.Any() ? string.Join(", ", astronaut.Bag.Items) : "none";
                sb.AppendLine($"Name: {astronaut.Name}");
                sb.AppendLine($"Oxygen: {astronaut.Oxygen}");
                sb.AppendLine($"Bag items: {bagItems}");
            }

            return sb.ToString().TrimEnd();
        }

        public string RetireAstronaut(string astronautName)
        {
            var astroToRemove = astronauts.FindByName(astronautName);
            if (astroToRemove == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRetiredAstronaut, astronautName));
            }

            astronauts.Remove(astroToRemove);

            return string.Format(OutputMessages.AstronautRetired, astronautName);
        }
    }
}

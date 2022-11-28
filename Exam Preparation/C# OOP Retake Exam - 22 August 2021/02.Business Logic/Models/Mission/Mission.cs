namespace SpaceStation.Models.Mission
{
    using System.Collections.Generic;
    using System.Linq;


    using Contracts;
    using SpaceStation.Models.Astronauts.Contracts;
    using SpaceStation.Models.Planets.Contracts;

    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            foreach (var astronaut in astronauts)
            {
                while (astronaut.CanBreath && planet.Items.Any())
                {
                    string item = planet.Items.First();

                    astronaut.Bag.Items.Add(item);
                    planet.Items.Remove(item);

                    astronaut.Breath();
                }
            }
        }
    }
}

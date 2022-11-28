namespace SpaceStation.Repositories
{
    using System.Collections.Generic;

    using Models.Planets.Contracts;
    using Contracts;
    using System.Linq;

    public class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> planets;

        public PlanetRepository()
        {
            planets= new List<IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models => planets.AsReadOnly();

        public void Add(IPlanet model)
        {
            planets.Add(model);
        }

        public IPlanet FindByName(string name)
        {
            return planets.FirstOrDefault(p => p.Name == name);
        }

        public bool Remove(IPlanet model)
        {
            return planets.Remove(model);
        }
    }
}

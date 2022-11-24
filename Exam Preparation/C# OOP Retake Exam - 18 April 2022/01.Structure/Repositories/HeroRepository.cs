

namespace Heroes.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models.Contracts;
  

    public class HeroRepository : IRepository<IHero>
    {
        private List<IHero> heroes;

        public HeroRepository()
        {
            heroes = new List<IHero>();
        }


        public IReadOnlyCollection<IHero> Models => heroes.AsReadOnly();

        public void Add(IHero model)
        {
            heroes.Add(model);
        }

        public IHero FindByName(string name)
        {
            return heroes.FirstOrDefault(h => h.Name == name);
        }

        public bool Remove(IHero model)
        {
            return heroes.Remove(model);
        }
    }
}

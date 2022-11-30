namespace Easter.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Models.Bunnies.Contracts;
    using Contracts;
  

    public class BunnyRepository : IRepository<IBunny>
    {
        private List<IBunny> bunnies;

        public BunnyRepository()
        {
            bunnies = new List<IBunny>();
        }

        public IReadOnlyCollection<IBunny> Models => bunnies.AsReadOnly();

        public void Add(IBunny model)
        {
            bunnies.Add(model);
        }

        public IBunny FindByName(string name)
        {
            return bunnies.FirstOrDefault(b => b.Name == name);
        }

        public bool Remove(IBunny model)
        {
            return bunnies.Remove(model);
        }
    }
}

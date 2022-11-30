namespace Easter.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Models.Eggs.Contracts;
    using Contracts;
    

    public class EggRepository : IRepository<IEgg>
    {
        private List<IEgg> eggs;

        public EggRepository()
        {
            eggs= new List<IEgg>();
        }

        public IReadOnlyCollection<IEgg> Models => eggs.AsReadOnly();

        public void Add(IEgg model)
        {
            eggs.Add(model);
        }

        public IEgg FindByName(string name)
        {
            return eggs.FirstOrDefault(e => e.Name == name);
        }

        public bool Remove(IEgg model)
        {
            return eggs.Remove(model);
        }
    }
}

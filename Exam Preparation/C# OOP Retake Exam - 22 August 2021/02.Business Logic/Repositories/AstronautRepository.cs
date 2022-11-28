namespace SpaceStation.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Models.Astronauts.Contracts;
    using Contracts;
  

    public class AstronautRepository : IRepository<IAstronaut>
    {
        private List<IAstronaut> models;

        public AstronautRepository()
        {
            models= new List<IAstronaut>();
        }

        public IReadOnlyCollection<IAstronaut> Models => models.AsReadOnly();

        public void Add(IAstronaut model)
        {
            models.Add(model);
        }

        public IAstronaut FindByName(string name)
        {
            return models.FirstOrDefault(m => m.Name == name);
        }

        public bool Remove(IAstronaut model)
        {
            return models.Remove(model);
        }
    }
}
